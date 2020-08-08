using QiQiTemplate.Model;
using System;

namespace QiQiTemplate.Filter
{
    /// <summary>
    /// 运算过滤器
    /// </summary>
    public class OperFilter : IFilter
    {
        /// <summary>
        /// 减法运算
        /// </summary>
        /// <param name="code"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public string Filter(object code, object[] args)
        {
            _ = args ?? throw new ArgumentNullException(nameof(args));
            _ = args.Length != 2 ? throw new ArgumentException(nameof(args)) : string.Empty;
            string oper = args[0].ToString();
            decimal num = code switch
            {
                object msg when msg is DynamicModel obj => Convert.ToDecimal(obj.FdValue),
                object msg when msg is int obj => obj,
                object msg when msg is decimal obj => obj,
                _ => throw new Exception($"{code}在调用运算过滤器时不是有效类型"),
            };
            decimal parame = args[1] switch
            {
                object msg when msg is DynamicModel obj => Convert.ToDecimal(obj.FdValue),
                object msg when msg is int obj => obj,
                object msg when msg is decimal obj => obj,
                _ => throw new Exception($"{args[1]}在调用运算过滤器时不是有效类型"),
            };
            return oper switch
            {
                "+" => (num + parame).ToString(),
                "-" => (num - parame).ToString(),
                "*" => (num * parame).ToString(),
                "/" => (num / parame).ToString(),
                _ => code?.ToString() ?? string.Empty,
            };
        }
    }
}