using QiQiTemplate.Enum;

namespace QiQiTemplate.Model
{
    /// <summary>
    /// 访问路径实体
    /// </summary>
    public class SourcePathModel
    {
        /// <summary>
        /// 类型
        /// </summary>
        public SourcePathType PathType { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string SourcePath { get; set; }
        /// <summary>
        /// 方法参数
        /// </summary>
        public SourcePathModel[] FuncParames { get; set; }
    }
}
