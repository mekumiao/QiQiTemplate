using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace QiQiTemplate
{
    public class ExpressionProvide
    {
        public static BlockExpression Each(Expression source, BlockExpression block, ParameterExpression val, ParameterExpression idx)
        {
            BinaryExpression init_arr = Expression.ArrayIndex(source, idx);
            BinaryExpression init_val = Expression.Assign(val, init_arr);
            BinaryExpression init_idx = Expression.Assign(idx, Expression.Constant(0));

            LabelTarget label = Expression.Label();
            MemberExpression length = Expression.Property(source, "Length");

            return Expression.Block(
                new[] { val, idx },
                init_idx,
                Expression.Loop(
                    Expression.IfThenElse(
                        Expression.LessThan(idx, length),
                        Expression.Block(init_val, block, Expression.PostIncrementAssign(idx)),
                        Expression.Break(label)
                    ),
                    label
                )
            );
        }

    }
}
