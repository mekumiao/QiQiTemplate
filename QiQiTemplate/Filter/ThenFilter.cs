using System;

namespace QiQiTemplate.Filter
{
    /// <summary>
    /// 三元表达式
    /// </summary>
    public class ThenFilter : IFilter
    {
        /// <summary>
        /// 表达式
        /// </summary>
        /// <param name="code"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public string Filter(object code, object[] args)
        {
            _ = args ?? throw new ArgumentNullException(nameof(args));
            _ = args.Length != 3 ? throw new ArgumentException(nameof(args)) : string.Empty;
            string left = code.ToString();
            string right = args[0].ToString();
            return left == right ? args[1].ToString() : args[2].ToString();
        }
    }
}