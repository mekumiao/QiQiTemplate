using QiQiTemplate.Model;
using QiQiTemplate.Provide;
using System;
using System.Text.RegularExpressions;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            var dyProvide = new DynamicModelProvide();//数据加载提供类
            var ndProvide = new NodeContextProvide();//模板编译提供类
            var outProvide = new OutPutProvide();//输出提供类
            var model = dyProvide.CreateByFilePath("folder/data.json");//加载数据

            Console.WriteLine(model.FdType);

            Print("folder/filtertemplate.txt", ndProvide, outProvide, model);
            Print("folder/alltemplate.txt", ndProvide, outProvide, model);
            Print("folder/eachtemplate.txt", ndProvide, outProvide, model);
            Print("folder/iftemplate.txt", ndProvide, outProvide, model);
            Print("folder/nestedtemplate.txt", ndProvide, outProvide, model);
            Print("folder/printtemplate.txt", ndProvide, outProvide, model);
            Print("folder/settemplate.txt", ndProvide, outProvide, model);

            Console.WriteLine(outProvide);

            static void Print(string temppath, NodeContextProvide nd, OutPutProvide output, DynamicModel data)
            {
                var lambda = nd.BuildTemplateByPath(temppath, output);
                lambda.Compile().Invoke(data);
            }
        }
    }
}
