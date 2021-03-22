using System.IO;

namespace QiQiTemplate.Factory
{
    /// <summary>
    /// 编译模板
    /// </summary>
    public class TemplateFactory
    {
        /// <summary>
        /// 创建并编译模板
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static Template CreateByCode(string code)
        {
            var temp = new Template(code);
            temp.Build();
            return temp;
        }
        /// <summary>
        /// 创建并编译模板
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Template CreateByPath(string path)
        {
            using var reader = new StreamReader(path);
            var code = reader.ReadToEnd();
            var temp = new Template(code);
            temp.Build();
            return temp;
        }
        /// <summary>
        /// 创建并编译模板
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static Template CreateByReader(StreamReader reader)
        {
            using var _reader = reader;
            var code = _reader.ReadToEnd();
            var temp = new Template(code);
            temp.Build();
            return temp;
        }
    }
}