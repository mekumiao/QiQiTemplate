using System;
using System.Collections.Generic;
using System.Text;

namespace QiQiTemplate.Filter
{
    /// <summary>
    /// 向左补位
    /// </summary>
    public class PadLeftFilter : IFilter
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name => "PadLeft";
        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="code"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public string Filter(string code, params object[] args)
        {
            return code.PadLeft((int)args[0], (char)args[1]);
        }
    }
}
