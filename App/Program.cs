using QiQiTemplate.Provide;
using System;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            var dyProvide = new DynamicModelProvide();//数据加载提供类
            var ndProvide = new NodeContextProvide();//模板编译提供类
            var outProvide = new OutPutProvide();//输出提供类

            //加载数据
            var model = dyProvide.CreateByFilePath("folder/data.json");
            ndProvide.BuildTemplateByPath("folder/alltemplate.txt", outProvide).Compile().Invoke(model);
            ndProvide.BuildTemplateByPath("folder/eachtemplate.txt", outProvide).Compile().Invoke(model);
            ndProvide.BuildTemplateByPath("folder/iftemplate.txt", outProvide).Compile().Invoke(model);
            ndProvide.BuildTemplateByPath("folder/nestedtemplate.txt", outProvide).Compile().Invoke(model);
            ndProvide.BuildTemplateByPath("folder/printtemplate.txt", outProvide).Compile().Invoke(model);
            ndProvide.BuildTemplateByPath("folder/settemplate.txt", outProvide).Compile().Invoke(model);
            Console.WriteLine(outProvide.ToString());
        }
    }
}
