using QiQiTemplate;
using System;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            //var msgs = new string[]
            //{
            //    "12",
            //    "-25412",
            //    "-0.325",
            //    "12.254",
            //    "0.1200",
            //    "false",
            //    "true",
            //    "-253",
            //    "number",
            //    "_amta0",
            //    "sdfs_sdf",
            //    "\"xxxsdfsd\"",
            //    "\"xxxx_x_efesdfsd\"",
            //};
            //foreach (var item in msgs)
            //{
            //    Console.WriteLine(TypeHelper.GetFieldTypeByValue(item));
            //}
            Console.WriteLine("---开始---");
            var provide = new NodeContextProvide();
            var coder = new CoderExpressionProvide();
            var lambda = provide.BuildTemplateByPath(@"Temp.txt", coder);
            var action = lambda.Compile();

            var model = new FieldDynamicModel();
            model.LoadByPath(@"Temp.json");

            action.Invoke(model);
            Console.WriteLine(coder.GetCode());
            Console.ReadLine();
        }
    }
}
