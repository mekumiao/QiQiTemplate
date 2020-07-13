using System;
using System.Collections.Generic;
using System.Text;

namespace QiQiTemplate.Filter
{
    /// <summary>
    /// 向右补位
    /// </summary>
    public class PadRightFilter : IFilter
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name => "PadRight";
        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="code"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public string Filter(string code, params object[] args)
        {
            return code.PadRight((int)args[0], (char)args[1]);
        }
    }
}
