using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QiQiTemplate.Filter
{
    /// <summary>
    /// 记录器
    /// </summary>
    public class RecorderFilter : IFilter
    {
        private readonly Dictionary<string, int> recorderdict = new Dictionary<string, int>(10);
        /// <summary>
        /// 名称
        /// </summary>
        public string Name => "recorder";
        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="code"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public string Filter(object code, object[] args)
        {
            var key = code.ToString();
            if (recorderdict.TryAdd(key, 0))
            {
                return key;
            }
            else
            {
                return $"{key}{++recorderdict[key]}";
            }
        }
    }
}
