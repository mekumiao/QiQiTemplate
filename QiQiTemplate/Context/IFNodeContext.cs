using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace QiQiTemplate
{
    public class IFNodeContext : NodeBlockContext, IParsing
    {
        public IFModel Model { get; private set; }

        public IFNodeContext(string code, NodeBlockContext parent)
            : base(code, parent)
        {
            ParsingModel();
            this.NdType = NodeType.IF;
        }

        public void ParsingModel()
        {

        }

        public override void ConvertToExpression()
        {
            throw new NotImplementedException();
        }
    }
}
