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
            _outPut = outPut;
        }
        /// <summary>
        /// 不换行输出
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public MethodCallExpression ExpressionPrint(Expression code)
        {
            return Expression.Call(Expression.Constant(_outPut), typeof(OutPutProvide).GetMethod("Print", new[] { typeof(object) }), code);
        }
        /// <summary>
        /// 不换行输出,带过滤器
        /// </summary>
        /// <param name="code"></param>
        /// <param name="filterName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public MethodCallExpression ExpressionPrint(Expression code, string filterName, Expression[] args)
        {
            return Expression.Call(Expression.Constant(_outPut), typeof(OutPutProvide).GetMethod("Print", new[] { typeof(object), typeof(string), typeof(object[]) }), new[]
            {
                code,
                Expression.Constant(filterName),
                Expression.NewArrayInit(typeof(object), args)
            });
        }
        /// <summary>
        /// 换行输出
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public MethodCallExpression ExpressionPrintLine(Expression code)
        {
            return Expression.Call(Expression.Constant(_outPut), typeof(OutPutProvide).GetMethod("PrintLine", new[] { typeof(object) }), code);
        }
        /// <summary>
        /// 空行
        /// </summary>
        /// <returns></returns>
        public MethodCallExpression ExpressionPrintLine()
        {
            return Expression.Call(Expression.Constant(_outPut), typeof(OutPutProvide).GetMethod("PrintLine", new Type[0]));
        }
    }
}