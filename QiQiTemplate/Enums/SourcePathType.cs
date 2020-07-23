namespace QiQiTemplate.Enums
{
    /// <summary>
    /// 访问路径类型
    /// </summary>
    public enum SourcePathType : byte
    {
        /// <summary>
        /// 索引
        /// </summary>
        Index,
        /// <summary>
        /// 属性
        /// </summary>
        Attribute,
        /// <summary>
        /// 变量(访问路径的第一个节点)
        /// </summary>
        Variable,
        /// <summary>
        /// 访问路径
        /// </summary>
        SourcePath,
    }
}
