using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace QiQiTemplate
{
    public class TypeHelper
    {
        public static FieldType GetFieldTypeByValue(string value)
        {
            return value switch
            {
                string msg when Regex.IsMatch(msg, @"^[-]?[\d]+$") => FieldType.Int32,
                string msg when Regex.IsMatch(msg, @"^[-]?[\d][\d]*([.][\d]+)?$") => FieldType.Decimal,
                string msg when Regex.IsMatch(msg, "^(?=\").*(?<=\")$") => FieldType.String,
                string msg when Regex.IsMatch(msg, @"^(true|false)$") => FieldType.Bool,
                string msg when Regex.IsMatch(msg, @"^[a-zA-Z_][\w]*([.][\w\[\]]+)*$") => FieldType.Variable,
                _ => throw new Exception($"{value}是不受支持的类型"),
            };
        }
    }
}
