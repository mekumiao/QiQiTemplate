using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace QiQiTemplate
{
    public class ELSENodeContext : NodeBlockContext
    {
        public ELSENodeContext(string code, NodeBlockContext parent)
            : base(code, parent)
        {
            this.NdType = NodeType.ELSE;
        }

        public override void ConvertToExpression()
        {
            throw new NotImplementedException();
        }
    }
}
