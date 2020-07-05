using QiQiTemplate.Enum;
using QiQiTemplate.Model;
using QiQiTemplate.Provide;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace QiQiTemplate.Context
{
    public class PRINTNodeContext : NodeContext
    {
        protected static readonly Regex ParsingRegex1 = new Regex(@"(?<={{)(.+?)(?=}})", RegexOptions.Compiled);
        protected static readonly Regex ParsingReges2 = new Regex(@"{{", RegexOptions.Compiled);

        public List<PrintModel> Model { get; private set; }

        public PRINTNodeContext(string code, NodeBlockContext parent,CoderExpressionProvide coder)
            : base(code, parent, coder)
        {
            ParsingModel();
            this.NdType = NodeType.PRINT;
        }

        private void MatchPrint(StringBuilder builder, List<PrintModel> prints)
        {
            var mth = ParsingReges2.Match(builder.ToString());
            if (mth.Success)
            {
                if (mth.Index > 0)
                {
                    prints.Add(new PrintModel
                    {
                        PtType = PrintType.String,
                        SourcePath = builder.ToString().Substring(0, mth.Index),
                    });
                    builder.Remove(0, mth.Index);
                    if (builder.Length > 0) MatchPrint(builder, prints);
                    return;
                }
            }
            mth = ParsingRegex1.Match(builder.ToString());
            if (mth.Success)
            {
                prints.Add(new PrintModel
                {
                    PtType = PrintType.Variable,
                    SourcePath = mth.Value,
                });
                builder.Remove(0, mth.Length + 4);
                if (builder.Length > 0) MatchPrint(builder, prints);
                return;
            }

        }

        protected override void ParsingModel()
        {
            var builder = new StringBuilder(this.CodeString);
            var list = new List<PrintModel>(10);
            this.Model = list;
            MatchPrint(builder, list);
        }

        public override void ConvertToExpression()
        {
            var exps = new List<Expression>(10);
            var pars = new List<ParameterExpression>(10);

            foreach (var item in this.Model)
            {
                switch (item.PtType)
                {
                    case PrintType.String:
                        MethodCallExpression print = this.CoderProvide.ExpressionPrint(Expression.Constant(item.SourcePath));
                        exps.Add(print);
                        break;
                    case PrintType.Variable:
                        ParameterExpression param = Expression.Variable(typeof(DynamicModel));
                        BlockExpression path = this.SearchPath(param, item.SourcePath);
                        print = this.CoderProvide.ExpressionPrint(param);
                        pars.Add(param);
                        exps.Add(path);
                        exps.Add(print);
                        break;
                    default:
                        break;
                }
            }
            MethodCallExpression printLine = this.CoderProvide.ExpressionPrintLine();
            exps.Add(printLine);
            this.NdExpression = Expression.Block(pars, exps);
        }
    }
}
