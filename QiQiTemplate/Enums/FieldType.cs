namespace QiQiTemplate.Enums
{
    /// <summary>
    /// 字段类型
    /// </summary>
    public enum FieldType : byte
    {
        /// <summary>
        /// 整数
        /// </summary>
        Int,
        /// <summary>
        /// 数字
        /// </summary>
        Decimal,
        /// <summary>
        /// 字符串
        /// </summary>
        String,
        /// <summary>
        /// 字符
        /// </summary>
        Char,
        /// <summary>
        /// bool
        /// </summary>
        Bool,
        /// <summary>
        /// 访问路径
        /// </summary>
        SourcePath,
        /// <summary>
        /// 数组类型
        /// </summary>
        Array,
        /// <summary>
        /// 对象类型
        /// </summary>
        Object,
    }
}