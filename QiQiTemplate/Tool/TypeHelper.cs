using QiQiTemplate.Enums;
using System;
using System.Text.RegularExpressions;

namespace QiQiTemplate.Tool
{
    /// <summary>
    /// 类型辅助
    /// </summary>
    public class TypeHelper
    {
        /// <summary>
        /// 获取字符串的类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FieldType GetFieldTypeByValue(ref string value)
        {
            FieldType type = value switch
            {
                string msg when Regex.IsMatch(msg, @"^[-]?[\d][\d]*([.][\d]+)?$") => FieldType.Decimal,
                string msg when Regex.IsMatch(msg, "^\".*\"$") => FieldType.String,
                string msg when Regex.IsMatch(msg, "^'.+'$") => FieldType.Char,
                string msg when Regex.IsMatch(msg, @"^(true|false)$") => FieldType.Bool,
                string msg when Regex.IsMatch(msg, @"^[-]?\d+$") => FieldType.Int,
                string msg when Regex.IsMatch(msg, @"^[a-zA-Z_][\w]*([.][\w\[\]]+)*$") => FieldType.SourcePath,
                _ => throw new Exception($"{value}是不受支持的类型"),
            };
            value = type switch
            {
                FieldType.String => value.Trim('"'),
                FieldType.Char => value.Trim('\''),
                _ => value,
            };
            return type;
        }
    }
}
