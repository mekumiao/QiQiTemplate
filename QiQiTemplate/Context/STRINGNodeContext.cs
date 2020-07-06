using QiQiTemplate.Enum;
using QiQiTemplate.Provide;
using System.Linq.Expressions;

namespace QiQiTemplate.Context
{
    public class STRINGNodeContext : NodeContext
    {
        public STRINGNodeContext(string code, NodeBlockContext parent, OutPutProvide output) :
            base(code, parent, output)
        {
            this.NdType = NodeType.STRING;
        }

        public override void ConvertToExpression()
        {
            this.NdExpression = this.CoderProvide.ExpressionPrintLine(Expression.Constant(this.CodeString));
        }

        protected override void ParsingModel()
        {
            //
        }
    }
}
