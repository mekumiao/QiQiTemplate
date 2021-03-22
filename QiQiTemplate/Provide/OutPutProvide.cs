using System.IO;
using System.Text;

namespace QiQiTemplate.Provide
{
    /// <summary>
    /// 输出类
    /// </summary>
    public class OutPutProvide
    {
        private readonly FilterProvide _filterProvide;
        private readonly StringBuilder _stringBuilder;
        /// <summary>
        /// 构造
        /// </summary>
        public OutPutProvide()
        {
            _filterProvide = new FilterProvide();
            _stringBuilder = new StringBuilder();
        }
        /// <summary>
        /// 根据StringBuilder构造
        /// </summary>
        /// <param name="builder"></param>
        public OutPutProvide(StringBuilder builder)
        {
            _filterProvide = new FilterProvide();
            _stringBuilder = builder;
        }
        /// <summary>
        /// 清空输出取
        /// </summary>
        public void Clear()
        {
            _filterProvide.Reset();
            _stringBuilder.Clear();
        }
        /// <summary>
        /// 输出到文件. 默认采用utf8
        /// </summary>
        /// <param name="path"></param>
        /// <param name="withbom">utf8格式是否带bom</param>
        public void OutPut(string path, bool withbom = false)
        {
            OutPut(path, new UTF8Encoding(withbom));
        }
        /// <summary>
        /// 指定编码输出
        /// </summary>
        /// <param name="path"></param>
        /// <param name="encoding"></param>
        public void OutPut(string path, Encoding encoding)
        {
            using var writer = new StreamWriter(path, false, encoding);
            writer.Write(ToString());
        }
        /// <summary>
        /// 输出
        /// </summary>
        /// <param name="code"></param>
        public void Print(object code)
        {
            _stringBuilder.Append(code.ToString());
        }
        /// <summary>
        /// 输出,带过滤器
        /// </summary>
        /// <param name="code"></param>
        /// <param name="filter"></param>
        /// <param name="args"></param>
        public void Print(object code, string filter, object[] args)
        {
            string msg = _filterProvide.GetFilter(filter).Filter(code, args);
            _stringBuilder.Append(msg);
        }
        /// <summary>
        /// 输出并换行
        /// </summary>
        /// <param name="code"></param>
        public void PrintLine(object code)
        {
            _stringBuilder.AppendLine(code.ToString());
        }
        /// <summary>
        /// 输出换行
        /// </summary>
        public void PrintLine()
        {
            _stringBuilder.AppendLine();
        }
        /// <summary>
        /// 将输入内容转为字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _stringBuilder.ToString().TrimEnd();
        }
    }
}