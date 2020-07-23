using System;
using System.Collections.Generic;
using System.Text;

namespace QiQiTemplate.Filter
{
    /// <summary>
    /// 转为小写
    /// </summary>
    public class ToLowerFilter : IFilter
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name => "tolower";
        /// <summary>
        /// 转为小写
        /// </summary>
        /// <param name="code"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public string Filter(object code, object[] args)
        {
            return code?.ToString().ToLower() ?? string.Empty;
        }
    }
}
