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
        //{{#else if idx >= 2 & xx >= xxx | xx <= xxx}} (?<logoper>&|\|)
        protected static readonly new Regex ParsingRegex = new Regex(@"((?<logoper>(\s[&|])?)\s(?<left>[^\s]+)\s(?<oper>>=|>|<|<=|==|!=)\s(?<right>[^|&}]+))+", RegexOptions.Compiled);

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
