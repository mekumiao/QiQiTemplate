using QiQiTemplate.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QiQiTemplate.Filter
{
    /// <summary>
    /// 拼接数组
    /// </summary>
    public class JoinFilter : IFilter
    {
        /// <summary>
        /// 拼接
        /// </summary>
        /// <param name="code"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public string Filter(object code, object[] args)
        {
            _ = args ?? throw new ArgumentNullException(nameof(args));
            _ = args.Length != 1 ? throw new ArgumentException(nameof(args)) : string.Empty;
            if (code is DynamicModel obj)
            {
                var arr = obj.GetValues().Select(x => x.FdValue.ToString());
                return string.Join(args[0].ToString(), arr);
            }
            return code?.ToString() ?? string.Empty;
        }
    }
}
