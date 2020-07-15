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
        /// 名称
        /// </summary>
        public string Name => "join";
        /// <summary>
        /// 拼接
        /// </summary>
        /// <param name="code"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public string Filter(object code, object[] args)
        {
            if (code is DynamicModel obj)
            {
                var arr = obj.GetValues().Select(x => x.FdValue.ToString());
                return string.Join(args[0].ToString(), arr);
            }
            return code.ToString();
        }
    }
}
