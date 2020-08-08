using System;

namespace QiQiTemplate.Filter
{
    /// <summary>
    /// 空值过滤器
    /// </summary>
    public class IsNullFilter : IFilter
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
            _ = args.Length != 1 ? throw new ArgumentException(nameof(args)) : string.Empty;
            if (code == null || string.IsNullOrWhiteSpace(code.ToString()))
            {
                return args[0].ToString();
            }
            return code.ToString();
        }
    }
}