using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace QiQiTemplate.Context
{
    public class STRINGNodeContext : NodeContext
    {
        public STRINGNodeContext(string code, NodeBlockContext parent, CoderExpressionProvide coder) :
            base(code, parent, coder)
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
