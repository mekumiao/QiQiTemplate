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
    }
}
