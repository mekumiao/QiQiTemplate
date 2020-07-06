using System;
using System.Linq.Expressions;

namespace QiQiTemplate.Provide
{
    public class CoderExpressionProvide
    {
        private readonly OutPutProvide _outPut;

        public CoderExpressionProvide(OutPutProvide outPut)
        {
            this._outPut = outPut;
        }

        public MethodCallExpression ExpressionPrint(Expression exp)
        {
            return Expression.Call(Expression.Constant(this._outPut), typeof(OutPutProvide).GetMethod("Print"), exp);
        }

        public MethodCallExpression ExpressionPrintLine(Expression exp)
        {
            return Expression.Call(Expression.Constant(this._outPut), typeof(OutPutProvide).GetMethod("PrintLine", new[] { typeof(object) }), exp);
        }

        public MethodCallExpression ExpressionPrintLine()
        {
            return Expression.Call(Expression.Constant(this._outPut), typeof(OutPutProvide).GetMethod("PrintLine", new Type[0]));
        }

    }
}
