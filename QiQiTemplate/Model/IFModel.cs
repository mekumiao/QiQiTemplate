using QiQiTemplate.Enums;

namespace QiQiTemplate.Model
{
    /// <summary>
    /// IF 节点
    /// </summary>
    public class IFModel
    {
        private string? _logOper;
        /// <summary>
        /// 左字段
        /// </summary>
        public string Left { get; set; } = string.Empty;
        /// <summary>
        /// 左字段类型
        /// </summary>
        public FieldType LeftType { get; set; }
        /// <summary>
        /// 逻辑运算符
        /// </summary>
        public string LogOper
        {
            get { return _logOper ?? "&"; }
            set
            {
                if (value != "&" || value != "|")
                {
                    _logOper = "&";
                }
                else
                {
                    _logOper = value;
                }
            }
        }
        /// <summary>
        /// 比较符
        /// </summary>
        public string Oper { get; set; } = string.Empty;
        /// <summary>
        /// 右字段
        /// </summary>
        public string Right { get; set; } = string.Empty;
        /// <summary>
        /// 右字段类型
        /// </summary>
        public FieldType RightType { get; set; }
    }
}