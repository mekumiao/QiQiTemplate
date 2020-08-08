using QiQiTemplate.Enums;

namespace QiQiTemplate.Model
{
    /// <summary>
    /// 变量定义节点
    /// </summary>
    public class DeFineModel
    {
        /// <summary>
        /// 变量名称
        /// </summary>
        public string ArgName { get; set; }
        /// <summary>
        /// 变量值
        /// </summary>
        public string ArgValue { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public FieldType FdType { get; set; }
    }
}