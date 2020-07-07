using QiQiTemplate;
using QiQiTemplate.Provide;
using System;
using System.Text;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            var dyProvide = new DynamicModelProvide();//数据加载类
            var outProvide = new OutPutProvide();//输出类
            var ndProvide = new NodeContextProvide();//模板编译类

            //加载数据
            var model = dyProvide.CreateByFilePath(@"Temp.json");
            //编译模板
            var action = ndProvide.BuildTemplateByPath(@"Temp.txt", outProvide).Compile();
            //执行
            action.Invoke(model);
            //输出到文件
            outProvide.OutPut(@"output.txt");
            //输出到控制台
            Console.Write(outProvide.ToString());
        }
    }
}
