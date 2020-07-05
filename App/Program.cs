using QiQiTemplate;
using QiQiTemplate.Provide;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            Print();
            Console.ReadLine();

            static void Print()
            {
                var ndProvide = new NodeContextProvide();
                var cdProvide = new CoderExpressionProvide();
                var dyProvide = new DynamicModelProvide();

                //加载数据
                var model = dyProvide.CreateByFilePath(@"Temp.json");
                //编译模板
                var action = ndProvide.BuildTemplateByPath(@"Temp.txt", cdProvide).Compile();

                //执行
                action.Invoke(model);
                //输出
                Console.Write(cdProvide.GetCode());
            }
        }
    }
}
