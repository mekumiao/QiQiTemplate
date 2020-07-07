using QiQiTemplate.Enum;

namespace QiQiTemplate.Model
{
    /// <summary>
    /// 输出节点实体
    /// </summary>
    public class PrintModel
    {
        /// <summary>
        /// 类型
        /// </summary>
        public PrintType PtType { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string SourcePath { get; set; }
    }
}
