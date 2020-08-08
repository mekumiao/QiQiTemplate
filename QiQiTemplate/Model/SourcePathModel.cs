using QiQiTemplate.Enums;

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
        /// 当访问节点是一个变量时,记录它的访问路径. 例如:
        /// 1._data.people[arr[0].value].name.
        /// 2.其中arr[0].value就是变量节点. 3.这样的情况还能无限的嵌套下去.
        /// </summary>
        public SourcePathModel[] ChildSourcePathModel { get; set; }
    }
}