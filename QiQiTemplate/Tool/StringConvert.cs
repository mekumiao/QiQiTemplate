using System;
using System.Collections.Generic;
using System.Text;

namespace QiQiTemplate.Tool
{
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
