namespace QiQiTemplate.Model
{
    /// <summary>
    /// EACH 节点
    /// </summary>
    public class EachModel
    {
        /// <summary>
        /// 循环索引
        /// </summary>
        public string IdxName { get; set; } = string.Empty;
        /// <summary>
        /// 数据源访问路径
        /// </summary>
        public string SourcePath { get; set; } = string.Empty;
        /// <summary>
        /// 循环值
        /// </summary>
        public string ValName { get; set; } = string.Empty;
    }
}