using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace QiQiTemplate
{
    public class DEFINENodeContext : NodeContext, IParsing
    {
        protected static readonly Regex ParsingRegex = new Regex(@"{{#define (?<type>[a-zA-Z]+)\s(?<name>[a-zA-Z_][\w]+)\s*=\s*(?<value>.+)}}", RegexOptions.Compiled);

        public DeFineModel Model { get; private set; }

        public DEFINENodeContext(string code, NodeBlockContext parent, CoderExpressionProvide coder)
            : base(code, parent, coder)
        {
            ParsingModel();
            this.NdType = NodeType.DEFINE;
        }

        public void ParsingModel()
        {
            var mth = ParsingRegex.Match(this.CodeString);
            this.Model = new DeFineModel
            {
                FdType = TypeHelper.GetTypeByString(mth.Groups["type"].Value),
                ArgName = mth.Groups["name"].Value,
                ArgVal = TypeHelper.GetObjectByString(mth.Groups["type"].Value, mth.Groups["value"].Value),
            };
        }

        public override void ConvertToExpression()
        {
            var left = Expression.Parameter(this.Model.FdType, this.Model.ArgName);
            var arg = Expression.Assign(left, Expression.Constant(this.Model.ArgVal));
            //将变量表达式加入到作用域中
            if (this.ParentNode is NodeBlockContext block)
            {
                block.Scope.Add(this.Model.ArgName, arg);
            }
            this.NdExpression = arg;
        }
    }
}
