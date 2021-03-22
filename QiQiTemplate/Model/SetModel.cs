namespace QiQiTemplate.Model
{
    /// <summary>
    /// 变量赋值和计算model
    /// </summary>
    public class SetModel
    {
        /// <summary>
        /// 变量名称
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 运算符号, 暂时只支持++,--
        /// </summary>
        public string Oper { get; set; } = string.Empty;
    }
}