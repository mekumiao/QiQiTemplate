using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using QiQiTemplate.Context;

namespace QiQiTemplate
{
    public class NodeContextProvide
    {
        public Expression<Action<FieldDynamicModel>> BuildTemplateByReader(StreamReader reader, CoderExpressionProvide coder)
        {
            var scope = new ScopeBlockContext();
            using var _reader = reader;
            var node = CreateNodeContextRange(_reader, scope, coder).First();
            return Expression.Lambda<Action<FieldDynamicModel>>(node.NdExpression, scope.Root);
        }

        public Expression<Action<FieldDynamicModel>> BuildTemplateByPath(string path, CoderExpressionProvide coder)
        {
            var reader = new StreamReader(path);
            return BuildTemplateByReader(reader, coder);
        }

        public Expression<Action<FieldDynamicModel>> BuildTemplateByString(string template, CoderExpressionProvide coder)
        {
            using var memory = new MemoryStream();
            using var writer = new StreamWriter(memory);
            writer.Write(template);
            memory.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(memory);
            return BuildTemplateByReader(reader, coder);
        }

        private List<NodeContext> CreateNodeContextRange(StreamReader reader, NodeBlockContext ParentNode, CoderExpressionProvide coder)
        {
            var nodes = new List<NodeContext>(10);
            while (true)
            {
                var line = reader.ReadLine();
                if (line == null) return nodes;
                var type = NodeContext.GetNodeType(line);
                switch (type)
                {
                    case NodeType.IF:
                        NodeBlockContext block = new IFNodeContext(line, ParentNode, coder);
                        block.Nodes = CreateNodeContextRange(reader, block, coder);
                        block.ConvertToExpression();
                        nodes.Add(block);
                        break;
                    case NodeType.ELSEIF:
                        block = new ELSEIFNodeContext(line, ParentNode, coder);
                        block.Nodes = CreateNodeContextRange(reader, block, coder);
                        block.ConvertToExpression();
                        nodes.Add(block);
                        break;
                    case NodeType.ELSE:
                        block = new ELSENodeContext(line, ParentNode, coder);
                        block.Nodes = CreateNodeContextRange(reader, block, coder);
                        block.ConvertToExpression();
                        nodes.Add(block);
                        break;
                    case NodeType.EACH:
                        block = new EACHNodeContext(line, ParentNode, coder);
                        block.Nodes = CreateNodeContextRange(reader, block, coder);
                        block.ConvertToExpression();
                        nodes.Add(block);
                        break;
                    case NodeType.PRINT:
                        NodeContext node = new PRINTNodeContext(line, ParentNode, coder);
                        node.ConvertToExpression();
                        nodes.Add(node);
                        break;
                    case NodeType.DEFINE:
                        node = new DEFINENodeContext(line, ParentNode, coder);
                        node.ConvertToExpression();
                        nodes.Add(node);
                        break;
                    case NodeType.STRING:
                        node = new STRINGNodeContext(line, ParentNode, coder);
                        node.ConvertToExpression();
                        nodes.Add(node);
                        break;
                    case NodeType.ENDIF:
                    case NodeType.ENDEACH:
                    default:
                        return nodes;
                }
            }
        }
    }
}
