namespace QiQiTemplate.Filter
{
    /// <summary>
    /// 首字母大写
    /// </summary>
    public class ToUpperCaseFilter : IFilter
    {
        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="code"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public string Filter(object code, object[] args)
        {
            string msg = code?.ToString() ?? string.Empty;
            if (msg.Length > 0)
            {
                string first = msg[0].ToString().ToUpper();
                string other = msg.Remove(0, 1).ToLower();
                return $"{first}{other}";
            }
            return msg;
        }
    }
}