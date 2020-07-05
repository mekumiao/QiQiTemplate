using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace QiQiTemplate
{
    public class IFNodeContext : NodeBlockContext
    {
        protected static readonly Regex ParsingRegex = new Regex(@"((?<logoper>(\s[&|])?)\s(?<left>[^\s]+)\s(?<oper>>=|>|<|<=|==|!=)\s(?<right>[^|&}]+))+", RegexOptions.Compiled);
        public List<IFModel> Model { get; protected set; }
        public NodeBlockContext ELSENode { get; set; }

        public IFNodeContext(string code, NodeBlockContext parent, CoderExpressionProvide coder)
            : base(code, parent, coder)
        {
            ParsingModel();
            this.NdType = NodeType.IF;
        }

        protected virtual string FormatCode()
        {
            return this.CodeString.Trim().Replace("{{#if", "");
        }

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

        public override void ConvertToExpression()
        {
            ParameterExpression comp = Expression.Variable(typeof(bool));
            BinaryExpression init_comp = Expression.Assign(comp, Expression.Constant(true));
            List<ParameterExpression> parames = new List<ParameterExpression>(10)
            {
                comp
            };
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
                        BinaryExpression assign = Expression.AndAssign(comp, binary);
                        inits.Add(assign);
                        break;
                    case "|":
                        assign = Expression.OrAssign(comp, binary);
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
                    var (param, init) = this.CreateDynamicModel(type, string.Empty, value);
                    parames.Add(param);
                    inits.Add(init);
                    return param;
                }
            }

            BlockExpression exptrue = this.MergeNodes();
            ConditionalExpression conditional = default;
            if (this.ELSENode != null) conditional = Expression.IfThenElse(comp, exptrue, this.ELSENode.NdExpression);
            else conditional = Expression.IfThen(comp, exptrue);
            inits.Add(conditional);
            this.NdExpression = Expression.Block(parames, inits);
        }
    }
}
