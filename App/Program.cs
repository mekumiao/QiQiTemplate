using QiQiTemplate;
using System;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            var tp = TypeHelper.GetFieldTypeByValue("2xx");
            Console.WriteLine(tp);

            //var node = new ELSEIFNodeContext("{{ idx >= 2}}", null);

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
