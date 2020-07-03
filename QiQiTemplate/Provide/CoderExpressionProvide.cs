using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace QiQiTemplate
{
    public class CoderExpressionProvide
    {
        private readonly StringBuilder stringBuilder = new StringBuilder();

        public void Print(object code)
        {
            stringBuilder.Append(code);
            //Console.Write(code);
        }

        public void PrintLine(object code)
        {
            stringBuilder.AppendLine($"{code}");
            //Console.WriteLine($"{code}");
        }

        public void PrintLine()
        {
            stringBuilder.AppendLine();
            //Console.WriteLine();
        }

        public MethodCallExpression ExpressionPrint(Expression exp)
        {
            return Expression.Call(Expression.Constant(this), typeof(CoderExpressionProvide).GetMethod("Print"), exp);
        }

        public MethodCallExpression ExpressionPrintLine(Expression exp)
        {
            return Expression.Call(Expression.Constant(this), typeof(CoderExpressionProvide).GetMethod("PrintLine", new[] { typeof(object) }), exp);
        }

        public MethodCallExpression ExpressionPrintLine()
        {
            return Expression.Call(Expression.Constant(this), typeof(CoderExpressionProvide).GetMethod("PrintLine", new Type[0]));
        }

        public string GetCode()
        {
            return this.stringBuilder.ToString();
        }
    }
}
