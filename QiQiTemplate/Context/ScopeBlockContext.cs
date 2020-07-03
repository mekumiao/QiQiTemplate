using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace QiQiTemplate
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
            this.Scope.Add("_data", Expression.Parameter(typeof(FieldDynamicModel), "_data"));
        }

        public override void ConvertToExpression()
        {
            throw new NotImplementedException("_data");
        }
    }
}
