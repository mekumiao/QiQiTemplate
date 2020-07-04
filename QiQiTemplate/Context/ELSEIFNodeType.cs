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

        public List<IFModel> Model { get; private set; }

        public ELSEIFNodeContext(string code, NodeBlockContext parent, CoderExpressionProvide coder)
            : base(code, parent, coder)
        {
            ParsingModel();
            this.NdType = NodeType.ELSEIF;
        }

        public void ParsingModel()
        {
            var mths = ParsingRegex.Matches(this.CodeString.Replace("{{#else if", ""));
            Model = new List<IFModel>(10);
            foreach (Match item in mths)
            {
                var md = new IFModel
                {
                    LogOper = item.Groups["logoper"].Value,
                    Left = item.Groups["left"].Value,
                    Right = item.Groups["right"].Value,
                    Oper = item.Groups["oper"].Value,
                };

                Model.Add(md);
            }
            this.Model = null;
        }

        public override void ConvertToExpression()
        {
            throw new NotImplementedException();
        }
    }
}
