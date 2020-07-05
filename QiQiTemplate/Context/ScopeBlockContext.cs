﻿using QiQiTemplate.Model;
using System.Linq.Expressions;

namespace QiQiTemplate.Context
{
    public class ScopeBlockContext : NodeBlockContext
    {
        public readonly string RootName = "_data";

        public ParameterExpression Root
        {
            get
            {
                this.Scope.TryGetValue(this.RootName, out var val);
                return val as ParameterExpression;
            }
        }

        public ScopeBlockContext() :
            base("_data", null, null)
        {
            this.Scope.Add("_data", Expression.Parameter(typeof(DynamicModel), "_data"));
        }

        public override void ConvertToExpression()
        {
            this.NdExpression = this.MergeNodes();
        }

        protected override void ParsingModel()
        {
            //不需额外实现
        }
    }
}
