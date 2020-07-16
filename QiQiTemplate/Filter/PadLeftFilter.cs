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
        public string Name => "padleft";
        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="code"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public string Filter(object code, object[] args)
        {
            _ = args ?? throw new ArgumentNullException(nameof(args));
            _ = args.Length != 2 ? throw new ArgumentException(nameof(args)) : string.Empty;
            int width = Convert.ToInt32(args[0]);
            char padding = Convert.ToChar(args[1]);
            return code?.ToString().PadLeft(width, padding) ?? string.Empty;
        }
    }
}
