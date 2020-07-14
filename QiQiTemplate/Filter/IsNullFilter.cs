using System;
using System.Collections.Generic;
using System.Text;

namespace QiQiTemplate.Filter
{
    /// <summary>
    /// 空值过滤器
    /// </summary>
    public class IsNullFilter : IFilter
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name => "isnull";
        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="code"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public string Filter(object code, object[] args)
        {
            if (code == null || string.IsNullOrWhiteSpace(code.ToString()))
            {
                return args[0].ToString();
            }
            return code.ToString();
        }
    }
}
