using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace QiQiTemplate
{
    public class ELSENodeContext : NodeBlockContext
    {
        public ELSENodeContext(string code, NodeBlockContext parent, CoderExpressionProvide coder)
            : base(code, parent, coder)
        {
            this.NdType = NodeType.ELSE;
        }

        public override void ConvertToExpression()
        {
            var exps = this.Nodes.Select(x => x.NdExpression);
            this.NdExpression = Expression.Block(exps);
        }
    }
}
