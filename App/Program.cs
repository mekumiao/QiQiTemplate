using QiQiTemplate;
using System;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            //var node = new ELSEIFNodeContext("{{ idx >= 2}}", null);

            var provide = new NodeContextProvide();

            var lambda = provide.BuildTemplateByPath(@"Temp.txt");
            var action = lambda.Compile();

            var model = new FieldDynamicModel();
            model.LoadByPath(@"Temp.json");

            action.Invoke(model);
        }
    }
}
