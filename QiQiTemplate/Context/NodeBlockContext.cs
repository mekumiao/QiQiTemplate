using System;
using System.Collections.Generic;
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

        public NodeBlockContext(string code, NodeBlockContext parent)
            : base(code, parent)
        {
            this.Scope = new Dictionary<string, Expression>(10);
            this.Nodes = new List<NodeContext>(10);
        }

        public Expression SearchVariable(string name)
        {
            return SearchVariable(name, this);
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
            return null;
        }
    }
}
