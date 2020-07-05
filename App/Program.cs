using QiQiTemplate;
using System;
using System.Linq.Expressions;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            var ndProvide = new NodeContextProvide();
            var cdProvide = new CoderExpressionProvide();
            var dyProvide = new DynamicModelProvide();

            var lambda = ndProvide.BuildTemplateByPath(@"Temp.txt", cdProvide);
            var action = lambda.Compile();

            var model = dyProvide.CreateByFilePath(@"Temp.json");
            action.Invoke(model);

            Console.WriteLine(cdProvide.GetCode());
        }
    }
}
