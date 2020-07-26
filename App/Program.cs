using QiQiTemplate.Factory;
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
            var dyProvide = new DynamicModelProvide();
            var model = dyProvide.CreateByFilePath("folder/data.json");
            FilterProvide.RegisFilter<QiQiTemplate.Filter.RecorderFilter>();
            var temp = TemplateFactory.CreateTemplateByPath("folder/eachtemplate.txt");

            //Print("folder/filtertemplate.txt", model);
            //Print("folder/alltemplate.txt", model);
            Print("folder/eachtemplate.txt", model);
            Print("folder/eachtemplate.txt", model);
            Print("folder/eachtemplate.txt", model);
            Print("folder/eachtemplate.txt", model);
            Print("folder/eachtemplate.txt", model);
            //Print("folder/iftemplate.txt", model);
            //Print("folder/nestedtemplate.txt", model);
            //Print("folder/printtemplate.txt", model);
            //Print("folder/settemplate.txt", model);

            void Print(string temppath, DynamicModel data)
            {
                //var temp = TemplateFactory.CreateTemplateByPath(temppath);
                var msg = temp.Invoke(data);
                Console.WriteLine(msg);
            }
        }
    }
}
