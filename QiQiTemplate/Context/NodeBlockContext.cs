using QiQiTemplate.Enums;
using QiQiTemplate.Provide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace QiQiTemplate.Context
{
    /// <summary>
    /// 块节点
    /// </summary>
    public abstract class NodeBlockContext : NodeContext
    {
        /// <summary>
        /// 初始化块节点
        /// </summary>
        /// <param name="code">代码串</param>
        /// <param name="parent">父节点</param>
        /// <param name="output">输出类</param>
        public NodeBlockContext(string code, NodeBlockContext? parent, OutPutProvide? output)
            : base(code, parent, output)
        {
            this.Scope = new Dictionary<string, ParameterExpression>(10);
            this.Nodes = new List<NodeContext>(10);
            this.DefineParams = new List<ParameterExpression>(10);
        }
        /// <summary>
        /// 存放范围内需要声明的变量
        /// </summary>
        public List<ParameterExpression> DefineParams { get; }
        /// <summary>
        /// 子节点
        /// </summary>
        public List<NodeContext> Nodes { get; set; }
        /// <summary>
        /// 存放变量的作用域.变量值类型统一转为.DynmicModel,Each的索引除外
        /// </summary>
        public Dictionary<string, ParameterExpression> Scope { get; }
        /// <summary>
        /// 递归的在父节点上搜索变量
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Expression SearchVariable(string name)
        {
            return this.SearchVariable(name, this) ?? throw new Exception($"作用域中没有找到{name}变量");
        }
        /// <summary>
        /// 尝试递归的在父节点上搜索变量
        /// </summary>
        /// <param name="name"></param>
        /// <param name="variable"></param>
        /// <returns></returns>
        public bool TrySearchVariable(string name, out ParameterExpression? variable)
        {
            variable = this.SearchVariable(name, this);
            return variable != null;
        }
        /// <summary>
        /// 合并当前块节点
        /// </summary>
        /// <returns></returns>
        protected BlockExpression MergeNodes()
        {
            var lst = this.Nodes.Where(x => x.NdType != NodeType.ELSEIF && x.NdType != NodeType.ELSE && x.NdExpression != null)
                .Select(x => x.NdExpression);
            return Expression.Block(this.DefineParams, lst);
        }
        /// <summary>
        /// 递归的在父节点上搜索变量
        /// </summary>
        /// <param name="name"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        private ParameterExpression? SearchVariable(string name, NodeContext node)
        {
            if (node != null && node is NodeBlockContext block)
            {
                if (block.Scope.TryGetValue(name, out var parame))
                {
                    return parame;
                }
                else
                {
                    return this.SearchVariable(name, node.ParentNode);
                }
            }
            return default;
        }
    }
}