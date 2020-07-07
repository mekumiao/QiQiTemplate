using QiQiTemplate.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace QiQiTemplate.Provide
{
    /// <summary>
    /// 表达式提供类
    /// </summary>
    public class ExpressionProvide
    {
        /// <summary>
        /// 创建访问DynamicModel的表达式
        /// </summary>
        /// <param name="parame"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static MethodCallExpression GetDynamicModelExpression(ParameterExpression parame, Expression value)
        {
            return Expression.Call(parame, typeof(DynamicModel).GetMethod("Get"), value);
        }
        /// <summary>
        /// 创建访问DynamicModel的表达式
        /// </summary>
        /// <param name="parame"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static MethodCallExpression GetDynamicModelExpression(ParameterExpression parame, string value)
        {
            return Expression.Call(parame, typeof(DynamicModel).GetMethod("Get"), Expression.Constant(new DynamicModel
            {
                FdValue = value
            }));
        }
        /// <summary>
        /// 创建访问DynamicModel的表达式
        /// </summary>
        /// <param name="parame"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static MethodCallExpression GetDynamicModelExpression(ParameterExpression parame, int value)
        {
            return Expression.Call(parame, typeof(DynamicModel).GetMethod("Get"), Expression.Constant(new DynamicModel
            {
                FdValue = value
            }));
        }
        /// <summary>
        /// 创建DynamicModel实例表达式
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static MemberInitExpression CreateDynamicModelExpression(Expression value, string name = "")
        {
            MemberAssignment bind1 = Expression.Bind(typeof(DynamicModel).GetProperty("FdName"), Expression.Constant(name));
            MemberAssignment bind2 = Expression.Bind(typeof(DynamicModel).GetProperty("FdValue"), value);
            return Expression.MemberInit(Expression.New(typeof(DynamicModel)), bind1, bind2);
        }
    }
}
