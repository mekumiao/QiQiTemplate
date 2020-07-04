using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace QiQiTemplate
{
    public class TypeHelper
    {
        public static Type GetTypeByString(string type)
        {
            return type.ToLower() switch
            {
                "bool" => Type.GetType("System.Boolean", true, true),
                "decimal" => Type.GetType("System.Decimal", true, true),
                "int" => Type.GetType("System.Int32", true, true),
                "long" => Type.GetType("System.Int64", true, true),
                "string" => Type.GetType("System.String", true, true),
                _ => throw new Exception($"不支持定义{type}类型"),
            };
        }

        public static object GetObjectByString(string type, string value)
        {
            return value switch
            {
                "bool" => Convert.ToBoolean(value),
                "decimal" => Convert.ToDecimal(value),
                "int" => Convert.ToInt32(value),
                "long" => Convert.ToInt64(value),
                "string" => value,
                _ => throw new Exception($"不支持定义{type}类型"),
            };
        }

        public static FieldType GetFieldTypeByValue(string value)
        {
            return value switch
            {
                string msg when Regex.IsMatch(msg, @"^[-]?[\d]+$") => FieldType.Int32,
                string msg when Regex.IsMatch(msg, @"^[-]?[\d][\d]*([.][\d]+)?$") => FieldType.Decimal,
                string msg when Regex.IsMatch(msg, "^(?=\").*(?<=\")$") => FieldType.String,
                string msg when Regex.IsMatch(msg, @"^(true|false)$") => FieldType.Bool,
                string msg when Regex.IsMatch(msg, @"^[a-zA-Z_][\w]*$") => FieldType.Variable,
                _ => throw new Exception($"{value}是不受支持的类型"),
            };
        }
    }
}
