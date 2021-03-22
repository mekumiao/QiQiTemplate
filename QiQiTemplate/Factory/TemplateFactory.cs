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
            string code = string.Empty;
            using (var reader = new StreamReader(path))
            {
                code = reader.ReadToEnd();
            }
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
            string code = string.Empty;
            using (var _reader = reader)
            {
                code = _reader.ReadToEnd();
            }
            var temp = new Template(code);
            temp.Build();
            return temp;
        }
    }
}