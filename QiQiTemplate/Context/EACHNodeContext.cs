using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace QiQiTemplate
{
    public class EACHNodeContext : NodeBlockContext, IParsing
    {
        protected static readonly Regex ParsingRegex = new Regex(@"{{#each (?<path>[^\s]+) (?<val>[^\s]+) (?<idx>[^\s]+)}}", RegexOptions.Compiled);

        protected ParameterExpression _val;
        protected ParameterExpression _idx;

        public EachModel Model { get; private set; }

        public EACHNodeContext(string code, NodeBlockContext parent, CoderExpressionProvide coder)
            : base(code, parent, coder)
        {
            ParsingModel();
            this.NdType = NodeType.EACH;
            this.BuildEachVariable();
        }

        public void ParsingModel()
        {
            var mth = ParsingRegex.Match(this.CodeString);
            this.Model = new EachModel
            {
                SourcePath = mth.Groups["path"].Value,
                ValName = mth.Groups["val"].Value,
                IdxName = mth.Groups["idx"].Value,
            };
        }

        public void BuildEachVariable()
        {
            ParameterExpression val = Expression.Variable(typeof(DynamicModel), this.Model.ValName);
            ParameterExpression idx = Expression.Variable(typeof(int), this.Model.IdxName);
            this.Scope.Add(this.Model.ValName, val);
            this.Scope.Add(this.Model.IdxName, idx);
        }

        public override void ConvertToExpression()
        {
            var param = Expression.Variable(typeof(DynamicModel));
            var path = this.SearchPath(param, this.Model.SourcePath);
            var block = Expression.Block(this.Nodes.Select(x => x.NdExpression));

            var val = this.SearchVariable(this.Model.ValName) as ParameterExpression;
            var idx = this.SearchVariable(this.Model.IdxName) as ParameterExpression;

            BinaryExpression init_idx = Expression.Assign(this.SearchVariable(this.Model.IdxName), Expression.Constant(0));
            MethodCallExpression init_arr = Expression.Call(param, typeof(DynamicModel).GetMethod("Get", new[] { typeof(int) }), idx);
            BinaryExpression init_val = Expression.Assign(val, init_arr);

            LabelTarget label = Expression.Label();
            MemberExpression count = Expression.Property(param, "Count");

            this.NdExpression = Expression.Block(
                new[] { param, val, idx },
                init_idx,
                path,
                Expression.Loop(
                    Expression.IfThenElse(
                        Expression.LessThan(idx, count),
                        Expression.Block(init_val, block, Expression.PostIncrementAssign(idx)),
                        Expression.Break(label)
                    ),
                    label
                )
            );
        }
    }
}
