using QiQiTemplate.Enum;
using QiQiTemplate.Provide;

namespace QiQiTemplate.Context
{
    /// <summary>
    /// ElseIf 节点
    /// </summary>
    public class ELSEIFNodeContext : IFNodeContext
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="code"></param>
        /// <param name="parent"></param>
        /// <param name="output"></param>
        public ELSEIFNodeContext(string code, NodeBlockContext parent, OutPutProvide output)
            : base(code, parent, output)
        {
            this.NdType = NodeType.ELSEIF;
        }
        /// <summary>
        /// 格式化code
        /// </summary>
        /// <returns></returns>
        protected override string FormatCode()
        {
            return this.CodeString.Trim().Replace("{{#elseif", "");
        }
    }
}
