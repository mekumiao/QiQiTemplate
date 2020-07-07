using QiQiTemplate.Enum;
using QiQiTemplate.Model;
using QiQiTemplate.Provide;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace QiQiTemplate.Context
{
    /// <summary>
    /// 语法节点抽象类
    /// </summary>
    public abstract class NodeContext
    {
        /// <summary>
        /// 输出提供类
        /// </summary>
        protected PrintExpressionProvide PrintProvide { get; }
        /// <summary>
        /// 节点ID
        /// </summary>
        public string NodeId { get; }
        /// <summary>
        /// 节点类型
        /// </summary>
        public NodeType NdType { get; protected set; }
        /// <summary>
        /// 节点表达式
        /// </summary>
        public Expression NdExpression { get; protected set; }
        /// <summary>
        /// 节点代码串
        /// </summary>
        public string CodeString { get; }
        /// <summary>
        /// 父节点
        /// </summary>
        public NodeBlockContext ParentNode { get; }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="code"></param>
        /// <param name="parent"></param>
        /// <param name="output"></param>
        public NodeContext(string code, NodeBlockContext parent, OutPutProvide output)
        {
            this.PrintProvide = new PrintExpressionProvide(output);
            this.NodeId = Guid.NewGuid().ToString("N");
            this.CodeString = code;
            this.ParentNode = parent;
        }

        /// <summary>
        /// 解析Code
        /// </summary>
        protected abstract void ParsingModel();

        /// <summary>
        /// 将节点转为Expression
        /// </summary>
        /// <returns></returns>
        public abstract void ConvertToExpression();

        /// <summary>
        /// 根据对象访问路径构建表达式
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <returns></returns>
        public (ParameterExpression param, BlockExpression init) SearchPath(string sourcePath)
        {
            var exps = new List<Expression>(10);
            var paths = SourcePathProvider.CreateSourcePath(sourcePath);
            ParameterExpression param = Expression.Variable(typeof(DynamicModel));

            Expression root = this.ParentNode.SearchVariable(paths[0].SourcePath);
            BinaryExpression init_param;
            if (root.Type == typeof(DynamicModel))
            {
                init_param = Expression.Assign(param, root);
                exps.Add(init_param);
            }
            else if (root.Type == typeof(int))
            {
                ParameterExpression idx = root as ParameterExpression;
                MemberAssignment bind1 = Expression.Bind(typeof(DynamicModel).GetProperty("FdName"), Expression.Constant(idx.Name));
                MemberAssignment bind2 = Expression.Bind(typeof(DynamicModel).GetProperty("FdValue"), Expression.Convert(idx, typeof(object)));
                MemberInitExpression init = Expression.MemberInit(Expression.New(typeof(DynamicModel)), bind1, bind2);
                init_param = Expression.Assign(param, init);
                exps.Add(init_param);
            }
            else
            {
                throw new Exception($"不支持{root.Type}类型的访问路径搜索");
            }

            for (int i = 1; i < paths.Length; i++)
            {
                SourcePathModel item = paths[i];
                Expression exppath;
                MethodCallExpression getCall;
                switch (item.PathType)
                {
                    case SourcePathType.Index:
                        exppath = Expression.Constant(Convert.ToInt32(item.SourcePath));
                        getCall = Expression.Call(param, typeof(DynamicModel).GetMethod("Get", new[] { typeof(int) }), exppath);
                        break;
                    case SourcePathType.Attribute:
                        exppath = Expression.Constant(item.SourcePath);
                        getCall = Expression.Call(param, typeof(DynamicModel).GetMethod("Get", new[] { typeof(string) }), exppath);
                        break;
                    case SourcePathType.Variable:
                        exppath = this.ParentNode.SearchVariable(item.SourcePath);
                        getCall = Expression.Call(param, typeof(DynamicModel).GetMethod("Get", new[] { exppath.Type }), exppath);
                        break;
                    default:
                        throw new Exception($"不支持{item.PathType}类型的访问路径");
                }
                init_param = Expression.Assign(param, getCall);
                exps.Add(init_param);
            }
            return (param, Expression.Block(exps));
        }
        /// <summary>
        /// 根据访问路径创建数据
        /// </summary>
        /// <param name="fdType"></param>
        /// <param name="fdName"></param>
        /// <param name="fdValue"></param>
        /// <returns></returns>
        protected (ParameterExpression param, Expression init) CreateDynamicModel(FieldType fdType, string fdName, string fdValue)
        {
            ParameterExpression param = Expression.Variable(typeof(DynamicModel));
            return fdType switch
            {
                FieldType.Int32 => (param, InitParame(Convert.ToInt32(fdValue))),
                FieldType.Decimal => (param, InitParame(Convert.ToDecimal(fdValue))),
                FieldType.String => (param, InitParame(fdValue)),
                FieldType.Bool => (param, InitParame(Convert.ToBoolean(fdValue))),
                FieldType.Variable => this.SearchPath(fdValue),
                _ => throw new Exception($"{fdType}是不受支持的字段类型"),
            };
            BinaryExpression InitParame<TValue>(TValue value)
            {
                MemberAssignment bind1 = Expression.Bind(typeof(DynamicModel).GetProperty("FdName"), Expression.Constant(fdName));
                MemberAssignment bind2 = Expression.Bind(typeof(DynamicModel).GetProperty("FdValue"), Expression.Convert(Expression.Constant(value), typeof(object)));
                MemberInitExpression init = Expression.MemberInit(Expression.New(typeof(DynamicModel)), bind1, bind2);
                return Expression.Assign(param, init);
            }
        }
    }
}
