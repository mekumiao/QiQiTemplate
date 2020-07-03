using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace QiQiTemplate
{
    public class ELSEIFNodeContext : NodeBlockContext, IParsing
    {
        //{{#else if idx >= 2 & xx >= xxx | xx <= xxx}} (?<logoper>&|\|)
        protected static readonly Regex ParsingRegex = new Regex(@"((?<logoper>(\s[&|])?)\s(?<left>[^\s]+)\s(?<oper>>=|>|<|<=|==|!=)\s(?<right>[^|&}]+))+", RegexOptions.Compiled);


        public IFModel Model { get; private set; }

        public ELSEIFNodeContext(string code, NodeBlockContext parent)
            : base(code, parent)
        {
            ParsingModel();
            this.NdType = NodeType.ELSEIF;
        }

        public void ParsingModel()
        {
            var mths = ParsingRegex.Matches(this.CodeString.Replace("{{#else if", ""));
            foreach (Match item in mths)
            {
                var gp = item.Groups;
            }
            this.Model = null;
        }

        public override void ConvertToExpression()
        {
            throw new NotImplementedException();
        }
    }
}
