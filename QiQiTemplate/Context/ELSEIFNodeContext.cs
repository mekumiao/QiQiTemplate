using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace QiQiTemplate
{
    public class ELSEIFNodeContext : IFNodeContext
    {
        public ELSEIFNodeContext(string code, NodeBlockContext parent, CoderExpressionProvide coder)
            : base(code, parent, coder)
        {
            this.NdType = NodeType.ELSEIF;
        }

        protected override string FormatCode()
        {
            return this.CodeString.Trim().Replace("{{#else if", "");
        }
    }
}
