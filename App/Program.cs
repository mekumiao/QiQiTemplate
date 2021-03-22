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

            static void Print(string temppath)
            {
                var dyProvide = new DynamicModelProvide();
                var data = dyProvide.CreateByFilePath("folder/data.json");
                var temp = TemplateFactory.CreateByPath(temppath);
                var msg = temp.Invoke(data);
                Console.WriteLine(msg);
            }
        }
    }
}