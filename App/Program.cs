using QiQiTemplate.Factory;
using QiQiTemplate.Provide;
using System;

namespace App
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Print("folder/filtertemplate.txt");
            Print("folder/alltemplate.txt");
            Print("folder/eachtemplate.txt");
            Print("folder/iftemplate.txt");
            Print("folder/nestedtemplate.txt");
            Print("folder/printtemplate.txt");
            Print("folder/settemplate.txt");

            static void Print(string tempPath)
            {
                //创建模板
                var temp = TemplateFactory.CreateByPath(tempPath);
                //创建数据
                var data = new DynamicModelProvide().CreateByPath("folder/data.json");
                //执行模板
                var msg = temp.Invoke(data);
                //打印结果
                Console.WriteLine(msg);
            }
        }
    }
}