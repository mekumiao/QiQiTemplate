using System;
using System.Collections.Generic;
using System.Text;

namespace QiQiTemplate.Filter
{
    /// <summary>
    /// 转为大写
    /// </summary>
    public class ToUpperFilter : IFilter
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name => "toupper";
        /// <summary>
        /// 转为大写
        /// </summary>
        /// <param name="code"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public string Filter(object code, object[] args)
        {
            return code?.ToString().ToUpper() ?? string.Empty;
        }
    }
}
