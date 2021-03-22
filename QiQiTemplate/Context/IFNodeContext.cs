using QiQiTemplate.Enums;
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
        /// 构造
        /// </summary>
        /// <param name="code"></param>
        /// <param name="parent"></param>
        /// <param name="output"></param>
        public IFNodeContext(string code, NodeBlockContext parent, OutPutProvide output)
            : base(code, parent, output)
        {
            NdType = NodeType.IF;
        }
        /// <summary>
        /// else 节点
        /// </summary>
        public NodeBlockContext? ELSENode { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        public List<IFModel>? Model { get; protected set; }
        /// <summary>
        /// 转换表达式
        /// </summary>
        public override void ConvertToExpression()
        {
            var (parme, init) = CreateConditionExpression();
            BlockExpression exptrue = MergeNodes();
            ConditionalExpression conditiona;
            if (ELSENode != null) conditiona = Expression.IfThenElse(parme, exptrue, ELSENode.NdExpression);
            else conditiona = Expression.IfThen(parme, exptrue);
            NdExpression = Expression.Block(new[] { parme }, init, conditiona);
        }
        /// <summary>
        /// 格式化
        /// </summary>
        /// <returns></returns>
        protected virtual string FormatCode()
        {
            return CodeString.Trim().Replace("{{#if", "");
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
                var left = item.Groups["left"].Value.Trim();
                var right = item.Groups["right"].Value.Trim();
                lst.Add(new IFModel
                {
                    LeftType = TypeHelper.GetFieldTypeByValue(ref left),
                    RightType = TypeHelper.GetFieldTypeByValue(ref right),
                    LogOper = item.Groups["logoper"].Value.Trim(),
                    Oper = item.Groups["oper"].Value,
                    Left = left,
                    Right = right
                });
            }
            Model = lst;
        }
        /// <summary>
        /// 创建条件表达式
        /// </summary>
        /// <returns></returns>
        private (ParameterExpression parme, Expression init) CreateConditionExpression()
        {
            ParameterExpression parme = Expression.Variable(typeof(bool));
            BinaryExpression init_comp = Expression.Assign(parme, Expression.Constant(true));
            List<ParameterExpression> parames = new List<ParameterExpression>(10);
            List<Expression> inits = new List<Expression>(10)
            {
                init_comp
            };
            foreach (var item in Model!)
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
                        var (param, init) = SearchPath(value);
                        parames.Add(param);
                        inits.Add(init);
                        return param;
                    }
                    else
                    {
                        var expressValue = CreateConstExpression(type, value);
                        return ConvertToDynamicModel(expressValue);
                    }
                }
            }
            return (parme, Expression.Block(parames, inits));
        }
    }
}