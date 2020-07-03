using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace QiQiTemplate.Context
{
    public class STRINGNodeContext : NodeContext
    {
        public STRINGNodeContext(string code, NodeBlockContext parent) :
            base(code, parent)
        {
            this.NdType = NodeType.STRING;
        }

        public override void ConvertToExpression()
        {
            this.NdExpression = this.CorderProvide.ExpressionPrintLine(Expression.Constant(this.CodeString));
        }
    }
}
