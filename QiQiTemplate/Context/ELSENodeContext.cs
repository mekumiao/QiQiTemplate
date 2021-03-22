using QiQiTemplate.Enums;
using QiQiTemplate.Provide;

namespace QiQiTemplate.Context
{
    /// <summary>
    /// elae 节点
    /// </summary>
    public class ELSENodeContext : NodeBlockContext
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="code"></param>
        /// <param name="parent"></param>
        /// <param name="output"></param>
        public ELSENodeContext(string code, NodeBlockContext parent, OutPutProvide output)
            : base(code, parent, output)
        {
            NdType = NodeType.ELSE;
        }
        /// <summary>
        /// 转换表达式
        /// </summary>
        public override void ConvertToExpression()
        {
            NdExpression = MergeNodes();
        }
    }
}