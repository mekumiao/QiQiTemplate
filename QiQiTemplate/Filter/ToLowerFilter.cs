namespace QiQiTemplate.Filter
{
    /// <summary>
    /// 转为小写
    /// </summary>
    public class ToLowerFilter : IFilter
    {
        /// <summary>
        /// 转为小写
        /// </summary>
        /// <param name="code"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public string Filter(object code, object[] args)
        {
            return code?.ToString().ToLower() ?? string.Empty;
        }
    }
}