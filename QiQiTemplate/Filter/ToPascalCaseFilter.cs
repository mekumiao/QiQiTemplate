using System.Text;
namespace QiQiTemplate.Filter
{
    /// <summary>
    /// 转为小写
    /// </summary>
    public class ToPascalCaseFilter : IFilter
    {
        /// <summary>
        /// 转为小写
        /// </summary>
        /// <param name="code"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public string Filter(object code, object[] args)
        {
            var builder = new StringBuilder();
            var filter = new ToUpperCaseFilter();
            var msg = code?.ToString() ?? string.Empty;
            var array = msg.Split('_');
            foreach (var item in array)
            {
                if (item.Length > 0)
                {
                    var first = item[0].ToString().ToUpper();
                    var other = item.Remove(0, 1);
                    builder.Append(first);
                    builder.Append(other);
                }
            }
            return builder.ToString();
        }
    }
}