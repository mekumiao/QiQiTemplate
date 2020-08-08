using System;

namespace QiQiTemplate.Filter
{
    /// <summary>
    /// 向右补位
    /// </summary>
    public class PadRightFilter : IFilter
    {
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
            return code?.ToString().PadRight(width, padding) ?? string.Empty;
        }
    }
}