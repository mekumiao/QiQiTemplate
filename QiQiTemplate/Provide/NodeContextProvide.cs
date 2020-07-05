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
        private int _lineNumber = 0;
        public Expression<Action<DynamicModel>> BuildTemplateByReader(StreamReader reader, CoderExpressionProvide coder)
        {
            var scope = new ScopeBlockContext();
            using var _reader = reader;
            CreateNodeContextRange(_reader, scope, coder);
            var node = scope.Nodes;
            scope.ConvertToExpression();
            return Expression.Lambda<Action<DynamicModel>>(scope.NdExpression, scope.Root);
        }

        public Expression<Action<DynamicModel>> BuildTemplateByPath(string path, CoderExpressionProvide coder)
        {
            var reader = new StreamReader(path);
            return BuildTemplateByReader(reader, coder);
        }

        public Expression<Action<DynamicModel>> BuildTemplateByString(string template, CoderExpressionProvide coder)
        {
            using var memory = new MemoryStream();
            using var writer = new StreamWriter(memory);
            writer.Write(template);
            memory.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(memory);
            return BuildTemplateByReader(reader, coder);
        }

        /// <summary>
        /// 将语法解析为节点树
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="ParentNode"></param>
        /// <param name="coder"></param>
        private void CreateNodeContextRange(StreamReader reader, NodeBlockContext ParentNode, CoderExpressionProvide coder)
        {
            while (true)
            {
                string line = reader.ReadLine();
                if (line == null) return;
                _lineNumber++;
                NodeType type = NodeContext.GetNodeType(line);
                NodeContext last = ParentNode.Nodes.LastOrDefault();
                if (last?.NdType == NodeType.IF || last?.NdType == NodeType.ELSEIF)
                {
                    if (type != NodeType.ELSEIF && type != NodeType.ELSE)
                    {
                        for (int i = ParentNode.Nodes.Count - 1; i >= 0; i--)
                        {
                            NodeContext item = ParentNode.Nodes[i];
                            if (item.NdType != NodeType.ELSEIF && item.NdType != NodeType.IF)
                                break;
                            item.ConvertToExpression();
                        }
                    }
                }
                switch (type)
                {
                    case NodeType.IF:
                        NodeBlockContext block = new IFNodeContext(line, ParentNode, coder);
                        CreateNodeContextRange(reader, block, coder);
                        //block.ConvertToExpression();
                        ParentNode.Nodes.Add(block);
                        break;
                    case NodeType.ELSEIF:
                        block = new ELSEIFNodeContext(line, ParentNode, coder);
                        CreateNodeContextRange(reader, block, coder);
                        //block.ConvertToExpression();
                        if (last is IFNodeContext ifnd1)
                        {
                            ifnd1.ELSENode = block;
                        }
                        else if (last is ELSEIFNodeContext elnd1)
                        {
                            elnd1.ELSENode = block;
                        }
                        else
                        {
                            throw new Exception($"第{_lineNumber}行语法错误,else if必须在 if 或 else if 之后");
                        }
                        ParentNode.Nodes.Add(block);
                        break;
                    case NodeType.ELSE:
                        block = new ELSENodeContext(line, ParentNode, coder);
                        CreateNodeContextRange(reader, block, coder);
                        block.ConvertToExpression();
                        if (last is IFNodeContext ifnd)
                        {
                            ifnd.ELSENode = block;
                        }
                        else if (last is ELSEIFNodeContext elnd)
                        {
                            elnd.ELSENode = block;
                        }
                        else
                        {
                            throw new Exception($"第{_lineNumber}行语法错误,else 必须在 if 或 else if之后");
                        }

                        for (int i = ParentNode.Nodes.Count - 1; i >= 0; i--)
                        {
                            NodeContext item = ParentNode.Nodes[i];
                            if (item.NdType != NodeType.ELSEIF && item.NdType != NodeType.IF)
                                break;
                            item.ConvertToExpression();
                        }

                        ParentNode.Nodes.Add(block);
                        break;
                    case NodeType.EACH:
                        block = new EACHNodeContext(line, ParentNode, coder);
                        CreateNodeContextRange(reader, block, coder);
                        block.ConvertToExpression();
                        ParentNode.Nodes.Add(block);
                        break;
                    case NodeType.PRINT:
                        NodeContext node = new PRINTNodeContext(line, ParentNode, coder);
                        node.ConvertToExpression();
                        ParentNode.Nodes.Add(node);
                        break;
                    case NodeType.DEFINE:
                        node = new DEFINENodeContext(line, ParentNode, coder);
                        node.ConvertToExpression();
                        ParentNode.Nodes.Add(node);
                        break;
                    case NodeType.STRING:
                        node = new STRINGNodeContext(line, ParentNode, coder);
                        node.ConvertToExpression();
                        ParentNode.Nodes.Add(node);
                        break;
                    case NodeType.ENDIF:
                    case NodeType.ENDELSEIF:
                    case NodeType.ENDELSE:
                    case NodeType.ENDEACH:
                        return;
                    default:
                        throw new Exception($"{type}是不受支持的节点类型");
                }
            }
        }
    }
}
