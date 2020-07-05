using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace QiQiTemplate
{
    public class DEFINENodeContext : NodeContext
    {
        protected static readonly Regex ParsingRegex = new Regex(@"{{#define (?<name>[a-zA-Z_][\w]+)\s=\s(?<value>.+)}}", RegexOptions.Compiled);
        protected static readonly Regex RegexValue = new Regex("(?<=\")(.*)(?=\")", RegexOptions.Compiled);

        public DeFineModel Model { get; private set; }

        public DEFINENodeContext(string code, NodeBlockContext parent, CoderExpressionProvide coder)
            : base(code, parent, coder)
        {
            ParsingModel();
            this.NdType = NodeType.DEFINE;
        }

        protected override void ParsingModel()
        {
            var mth = ParsingRegex.Match(this.CodeString);
            this.Model = new DeFineModel
            {
                FdType = TypeHelper.GetFieldTypeByValue(mth.Groups["value"].Value.Trim()),
                ArgName = mth.Groups["name"].Value.Trim().Replace("\"", ""),
                ArgValue = mth.Groups["value"].Value.Trim(),
            };
            if (this.Model.FdType == FieldType.String)
            {
                this.Model.ArgValue = RegexValue.Match(this.Model.ArgValue).Value;
            }
        }

        public override void ConvertToExpression()
        {
            var (param, init) = this.CreateDynamicModel(this.Model.FdType, this.Model.ArgName, this.Model.ArgValue);
            //将变量表达式加入到作用域中
            if (this.ParentNode is NodeBlockContext block)
            {
                block.Scope.Add(this.Model.ArgName, param);
                block.DefineParams.Add(param);
            }
            else
            {
                throw new Exception($"defind节点的父节点必须是块级节点");
            }
            this.NdExpression = init;
        }
    }
}
