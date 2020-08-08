using QiQiTemplate.Model;
using QiQiTemplate.Provide;
using System;
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
        /// <param name="tempstring"></param>
        /// <returns></returns>
        public static Template CreateTemplate(string tempstring)
        {
            var temp = new Template(tempstring);
            temp.Build();
            return temp;
        }
        /// <summary>
        /// 创建并编译模板
        /// </summary>
        /// <param name="temppath"></param>
        /// <returns></returns>
        public static Template CreateTemplateByPath(string temppath)
        {
            var temp = new Template();
            temp.BuildByPath(temppath);
            return temp;
        }
        /// <summary>
        /// 创建并编译模板
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static Template CreateTemplateByReader(StreamReader reader)
        {
            var temp = new Template(reader);
            temp.Build();
            return temp;
        }
    }
    /// <summary>
    /// 模板
    /// </summary>
    public class Template
    {
        private readonly string tempString;
        private readonly StreamReader tempReader;
        private readonly DynamicModelProvide modelProvide = new DynamicModelProvide();
        private readonly NodeContextProvide contextProvide = new NodeContextProvide();
        private readonly OutPutProvide putProvide = new OutPutProvide();
        private Action<DynamicModel> templateAction;
        /// <summary>
        /// 构造
        /// </summary>
        public Template() { }
        /// <summary>
        /// 根据模板字符串创建模板
        /// </summary>
        /// <param name="tempstring"></param>
        public Template(string tempstring)
        {
            this.tempString = tempstring;
        }
        /// <summary>
        /// 根据模板reader创建模板
        /// </summary>
        /// <param name="tempreader"></param>
        public Template(StreamReader tempreader)
        {
            this.tempReader = tempreader;
        }
        /// <summary>
        /// 编译模板
        /// </summary>
        public void Build()
        {
            if (this.tempString != null)
            {
                this.templateAction = contextProvide.BuildTemplateByString(this.tempString, putProvide).Compile();
            }
            else
            {
                this.templateAction = contextProvide.BuildTemplateByReader(this.tempReader, putProvide).Compile();
            }
        }
        /// <summary>
        /// 根据模板路径创建模板
        /// </summary>
        /// <param name="path"></param>
        public void BuildByPath(string path)
        {
            this.templateAction = contextProvide.BuildTemplateByPath(path, putProvide).Compile();
        }
        /// <summary>
        /// 执行模板
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public string Invoke(string json)
        {
            var model = modelProvide.CreateByJson(json);
            return this.Invoke(model);
        }
        /// <summary>
        /// 执行模板
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Invoke(DynamicModel model)
        {
            templateAction?.Invoke(model);
            var outstring = putProvide.ToString();
            putProvide.Clear();
            return outstring;
        }
    }
}