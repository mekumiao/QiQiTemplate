using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace QiQiTemplate
{
    public abstract class NodeBlockContext : NodeContext
    {
        /// <summary>
        /// 存放变量的作用域
        /// </summary>
        public Dictionary<string, Expression> Scope { get; }

        public List<NodeContext> Nodes { get; set; }

        public NodeBlockContext(string code, NodeBlockContext parent, CoderExpressionProvide coder)
            : base(code, parent, coder)
        {
            this.Scope = new Dictionary<string, Expression>(10);
            this.Nodes = new List<NodeContext>(10);
        }

        public Expression SearchVariable(string name)
        {
            return SearchVariable(name, this);
        }

        protected BlockExpression MergeNodes()
        {
            var lst = this.Nodes.Where(x => x.NdType != NodeType.ELSEIF && x.NdType != NodeType.ELSE).Select(x => x.NdExpression);
            return Expression.Block(lst);
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
