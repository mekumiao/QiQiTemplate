using QiQiTemplate.Enum;
using QiQiTemplate.Model;
using QiQiTemplate.Provide;
using QiQiTemplate.Tool;
using System;
using System.Text.RegularExpressions;

namespace QiQiTemplate.Context
{
    /// <summary>
    /// 定义变量节点
    /// </summary>
    public class DEFINENodeContext : NodeContext
    {
        /// <summary>
        /// 正则
        /// </summary>
        protected static readonly Regex ParsingRegex = new Regex(@"{{#define (?<name>[a-zA-Z_][\w]+)\s=\s(?<value>.+)}}", RegexOptions.Compiled);
        /// <summary>
        /// 正则
        /// </summary>
        protected static readonly Regex RegexValue = new Regex("(?<=\")(.*)(?=\")", RegexOptions.Compiled);
        /// <summary>
        /// 信息
        /// </summary>
        public DeFineModel Model { get; private set; }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="code"></param>
        /// <param name="parent"></param>
        /// <param name="output"></param>
        public DEFINENodeContext(string code, NodeBlockContext parent, OutPutProvide output)
            : base(code, parent, output)
        {
            this.NdType = NodeType.DEFINE;
        }
        /// <summary>
        /// 解析
        /// </summary>
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
        /// <summary>
        /// 转换表达式
        /// </summary>
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
