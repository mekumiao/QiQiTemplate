using QiQiTemplate.Enum;
using QiQiTemplate.Model;
using QiQiTemplate.Provide;
using QiQiTemplate.Tool;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace QiQiTemplate.Context
{
    /// <summary>
    /// if 节点
    /// </summary>
    public class IFNodeContext : NodeBlockContext
    {
        /// <summary>
        /// 正则
        /// </summary>
        protected static readonly Regex ParsingRegex = new Regex(@"((?<logoper>(\s[&|])?)\s(?<left>[^\s]+)\s(?<oper>>=|>|<|<=|==|!=)\s(?<right>[^|&}]+))+", RegexOptions.Compiled);
        /// <summary>
        /// 信息
        /// </summary>
        public List<IFModel> Model { get; protected set; }
        /// <summary>
        /// else 节点
        /// </summary>
        public NodeBlockContext ELSENode { get; set; }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="code"></param>
        /// <param name="parent"></param>
        /// <param name="output"></param>
        public IFNodeContext(string code, NodeBlockContext parent, OutPutProvide output)
            : base(code, parent, output)
        {
            this.NdType = NodeType.IF;
        }
        /// <summary>
        /// 格式化
        /// </summary>
        /// <returns></returns>
        protected virtual string FormatCode()
        {
            return this.CodeString.Trim().Replace("{{#if", "");
        }
        /// <summary>
        /// 解析
        /// </summary>
        protected override void ParsingModel()
        {
            var mths = ParsingRegex.Matches(FormatCode());
            var lst = new List<IFModel>(10);
            foreach (Match item in mths)
            {
                var md = new IFModel
                {
                    LogOper = item.Groups["logoper"].Value.Trim(),
                    Left = item.Groups["left"].Value.Trim(),
                    Right = item.Groups["right"].Value.Trim(),
                    Oper = item.Groups["oper"].Value.Trim(),
                };
                md.LeftType = TypeHelper.GetFieldTypeByValue(md.Left);
                md.RightType = TypeHelper.GetFieldTypeByValue(md.Right);
                lst.Add(md);
            }
            this.Model = lst;
        }
        /// <summary>
        /// 创建条件表达式
        /// </summary>
        /// <returns></returns>
        protected (ParameterExpression parme, Expression init) CreateConditionExpression()
        {
            ParameterExpression parme = Expression.Variable(typeof(bool));
            BinaryExpression init_comp = Expression.Assign(parme, Expression.Constant(true));
            List<ParameterExpression> parames = new List<ParameterExpression>(10);
            List<Expression> inits = new List<Expression>(10)
            {
                init_comp
            };
            foreach (var item in this.Model)
            {
                BinaryExpression binary = item.Oper switch
                {
                    ">" => InitField(Expression.GreaterThan),
                    ">=" => InitField(Expression.GreaterThanOrEqual),
                    "<" => InitField(Expression.LessThan),
                    "<=" => InitField(Expression.LessThanOrEqual),
                    "==" => InitField(Expression.Equal),
                    "!=" => InitField(Expression.NotEqual),
                    _ => throw new Exception($"{item.Oper}是不受支持的运算符"),
                };
                switch (item.LogOper)
                {
                    case "&":
                        BinaryExpression assign = Expression.AndAssign(parme, binary);
                        inits.Add(assign);
                        break;
                    case "|":
                        assign = Expression.OrAssign(parme, binary);
                        inits.Add(assign);
                        break;
                    default:
                        throw new Exception($"{item.LogOper}是不受支持的逻辑运算符");
                }
                BinaryExpression InitField(Func<Expression, Expression, BinaryExpression> func)
                {
                    return func.Invoke(GetExpressionByIFModel(item.LeftType, item.Left), GetExpressionByIFModel(item.RightType, item.Right));
                }
                Expression GetExpressionByIFModel(FieldType type, string value)
                {
                    if (type == FieldType.SourcePath)
                    {
                        var (param, init) = this.GetConstByFd(type, value);
                        parames.Add(param as ParameterExpression);
                        inits.Add(init);
                        return param;
                    }
                    else
                    {
                        return this.GetConstByFd(type, value).value;
                    }
                }
            }
            return (parme, Expression.Block(parames, inits));
        }
        /// <summary>
        /// 转换表达式
        /// </summary>
        public override void ConvertToExpression()
        {
            var (parme, init) = this.CreateConditionExpression();
            BlockExpression exptrue = this.MergeNodes();
            ConditionalExpression conditiona;
            if (this.ELSENode != null) conditiona = Expression.IfThenElse(parme, exptrue, this.ELSENode.NdExpression);
            else conditiona = Expression.IfThen(parme, exptrue);
            this.NdExpression = Expression.Block(new[] { parme }, init, conditiona);
        }
    }
}
