using QiQiTemplate.Enum;
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
        /// 子节点
        /// </summary>
        public List<NodeContext> Nodes { get; set; }
        /// <summary>
        /// 存放变量的作用域
        /// </summary>
        public Dictionary<string, Expression> Scope { get; }
        /// <summary>
        /// 存放范围内需要声明的变量
        /// </summary>
        public List<ParameterExpression> DefineParams { get; }

        /// <summary>
        /// 初始化块节点
        /// </summary>
        /// <param name="code">代码串</param>
        /// <param name="parent">父节点</param>
        /// <param name="output">输出类</param>
        public NodeBlockContext(string code, NodeBlockContext parent, OutPutProvide output)
            : base(code, parent, output)
        {
            this.Scope = new Dictionary<string, Expression>(10);
            this.Nodes = new List<NodeContext>(10);
            this.DefineParams = new List<ParameterExpression>(10);
        }

        /// <summary>
        /// 递归的在父节点上搜索变量
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Expression SearchVariable(string name)
        {
            return SearchVariable(name, this);
        }

        /// <summary>
        /// 合并当前块节点
        /// </summary>
        /// <returns></returns>
        protected BlockExpression MergeNodes()
        {
            var lst = this.Nodes.Where(x => x.NdType != NodeType.ELSEIF && x.NdType != NodeType.ELSE).Select(x => x.NdExpression);
            return Expression.Block(this.DefineParams, lst);
        }

        private Expression SearchVariable(string name, NodeContext node)
        {
            if (node != null && node is NodeBlockContext block)
            {
                if (block.Scope.TryGetValue(name, out var exp))
                {
                    return exp;
                }
                else
                {
                    return SearchVariable(name, node.ParentNode);
                }
            }
            throw new Exception($"作用域中没有找到{name}变量");
        }
    }
}
