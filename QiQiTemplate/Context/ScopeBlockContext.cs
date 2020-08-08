using QiQiTemplate.Model;
using System.Linq.Expressions;

namespace QiQiTemplate.Context
{
    /// <summary>
    /// 范围节点
    /// </summary>
    public class ScopeBlockContext : NodeBlockContext
    {
        /// <summary>
        /// 根数据访问名称
        /// </summary>
        public readonly string RootName = "_data";
        /// <summary>
        /// 构造
        /// </summary>
        public ScopeBlockContext() :
            base("_data", null, null)
        {
            this.Scope.Add("_data", Expression.Parameter(typeof(DynamicModel), "_data"));
        }
        /// <summary>
        /// 根数据
        /// </summary>
        public ParameterExpression Root
        {
            get
            {
                this.Scope.TryGetValue(this.RootName, out var val);
                return val;
            }
        }
        /// <summary>
        /// 转换表达式
        /// </summary>
        public override void ConvertToExpression()
        {
            this.NdExpression = this.MergeNodes();
        }
        /// <summary>
        /// 解析
        /// </summary>
        protected override void ParsingModel()
        {
            //不需额外实现
        }
    }
}