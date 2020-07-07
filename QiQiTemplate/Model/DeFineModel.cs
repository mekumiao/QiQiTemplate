using QiQiTemplate.Enum;

namespace QiQiTemplate.Model
{
    /// <summary>
    /// 变量定义节点
    /// </summary>
    public class DeFineModel
    {
        /// <summary>
        /// 类型
        /// </summary>
        public FieldType FdType { get; set; }
        /// <summary>
        /// 变量名称
        /// </summary>
        public string ArgName { get; set; }
        /// <summary>
        /// 变量值
        /// </summary>
        public string ArgValue { get; set; }
    }
}
