using System;
using System.Linq.Expressions;

namespace QiQiTemplate.Provide
{
    /// <summary>
    /// 输出表达式提供类
    /// </summary>
    public class PrintExpressionProvide
    {
        private readonly OutPutProvide _outPut;

        /// <summary>
        /// 指定输出类创建表达式提供类
        /// </summary>
        /// <param name="outPut"></param>
        public PrintExpressionProvide(OutPutProvide outPut)
        {
            this._outPut = outPut;
        }

        /// <summary>
        /// 不换行
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public MethodCallExpression ExpressionPrint(Expression exp)
        {
            return Expression.Call(Expression.Constant(this._outPut), typeof(OutPutProvide).GetMethod("Print"), exp);
        }

        /// <summary>
        /// 换行
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public MethodCallExpression ExpressionPrintLine(Expression exp)
        {
            return Expression.Call(Expression.Constant(this._outPut), typeof(OutPutProvide).GetMethod("PrintLine", new[] { typeof(object) }), exp);
        }

        /// <summary>
        /// 空行
        /// </summary>
        /// <returns></returns>
        public MethodCallExpression ExpressionPrintLine()
        {
            return Expression.Call(Expression.Constant(this._outPut), typeof(OutPutProvide).GetMethod("PrintLine", new Type[0]));
        }

    }
}
