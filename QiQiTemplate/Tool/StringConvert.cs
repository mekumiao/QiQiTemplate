using System;
using System.Collections.Generic;
using System.Text;

namespace QiQiTemplate.Tool
{
    /// <summary>
    /// 字符转义
    /// </summary>
    public class StringConvert
    {
        /// <summary>
        /// { 和 }的转义
        /// </summary>
        /// <returns></returns>
        public static string Convert1(string value)
        {
            return value.Replace(@"\{", "{").Replace(@"\}", "}");
        }
    }
}
