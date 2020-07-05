using System;
using System.Linq.Expressions;
using System.Text;

namespace QiQiTemplate.Provide
{
    public class CoderExpressionProvide
    {
        private readonly StringBuilder stringBuilder = new StringBuilder();

        public void Print(object code)
        {
            stringBuilder.Append(code);
        }

        public void PrintLine(object code)
        {
            stringBuilder.AppendLine($"{code}");
        }

        public void PrintLine()
        {
            stringBuilder.AppendLine();
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
            return this.stringBuilder.ToString().TrimEnd('\n').TrimEnd('\r');
        }
    }
}
