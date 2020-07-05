using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace QiQiTemplate
{
    public class BuilderTest
    {
        public static Action<DynamicModel> Builder()
        {
            ParameterExpression _data = Expression.Parameter(typeof(DynamicModel), "_data");
            List<Expression> expressions = new List<Expression>(10);

            MemberExpression nowmodel = Expression.PropertyOrField(_data, "NowModel");

            MethodCallExpression path1 = Expression.Call(nowmodel, typeof(DynamicModel).GetMethod("Get", new[] { typeof(string) }), Expression.Constant("usings"));
            expressions.Add(path1);

            ParameterExpression root = Expression.Variable(typeof(DynamicModel), "root");

            BinaryExpression init_root = Expression.Assign(root, nowmodel);
            expressions.Add(init_root);

            MethodCallExpression resetCall = Expression.Call(_data, typeof(DynamicModel).GetMethod("Reset"));
            expressions.Add(resetCall);

            ParameterExpression idx = Expression.Variable(typeof(int), "idx");
            BinaryExpression init_idx = Expression.Assign(idx, Expression.Constant(0));

            ParameterExpression val = Expression.Variable(typeof(DynamicModel), "val");
            MethodCallExpression init_arr = Expression.Call(root, typeof(DynamicModel).GetMethod("Get", new[] { typeof(int) }), idx);
            BinaryExpression init_val = Expression.Assign(val, init_arr);

            LabelTarget label = Expression.Label();
            MemberExpression count = Expression.Property(root, "Count");

            CoderExpressionProvide coder = new CoderExpressionProvide();
            MethodCallExpression print1 = coder.ExpressionPrintLine(Expression.Constant("using "));
            MethodCallExpression print2 = coder.ExpressionPrint(val);
            MethodCallExpression print3 = coder.ExpressionPrint(Expression.Constant(";"));

            BlockExpression block = Expression.Block(print1, print2, print3);

            BlockExpression loop1 = Expression.Block(
                new[] { val, idx },
                init_idx,
                Expression.Loop(
                    Expression.IfThenElse(
                        Expression.LessThan(idx, count),
                        Expression.Block(init_val, block, Expression.PostIncrementAssign(idx)),
                        Expression.Break(label)
                    ),
                    label
                )
            );
            expressions.Add(loop1);

            //MethodCallExpression print4 = coder.ExpressionPrintLine();
            //expressions.Add(print4);
            //MethodCallExpression print5 = coder.ExpressionPrintLine(Expression.Constant("namespace "));
            //expressions.Add(print5);

            //BlockExpression path2 = ExpressionProvide.ByPath(_data, "_data.namespace");
            //expressions.Add(path2);

            //MemberExpression model2 = Expression.Property(_data, "NowModel");

            //MethodCallExpression print6 = coder.ExpressionPrintLine(model2);
            //expressions.Add(print6);

            var result = Expression.Block(new[] { root }, expressions);
            var lambda = Expression.Lambda<Action<DynamicModel>>(result, _data);
            return lambda.Compile();
        }
    }
}
