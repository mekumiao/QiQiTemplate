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
            this.ParsingModel();
        }
        /// <summary>
        /// 解析Code
        /// </summary>
        protected virtual void ParsingModel()
        {

        }
        /// <summary>
        /// 将节点转为Expression
        /// </summary>
        /// <returns></returns>
        public abstract void ConvertToExpression();
        /// <summary>
        /// 根据对象访问路径构建表达式
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public (ParameterExpression param, BlockExpression init) SearchPath(string sourcePath, ParameterExpression param = null)
        {
            var exps = new List<Expression>(10);
            var paths = SourcePathProvider.CreateSourcePath(sourcePath);
            if (paths.Length < 1) throw new Exception($"{sourcePath}访问路径不存在,或附近有语法错误");
            if (param == null)
                param = Expression.Variable(typeof(DynamicModel));

            Expression root = this.ParentNode.SearchVariable(paths[0].SourcePath);
            BinaryExpression init_param;
            if (root.Type == typeof(DynamicModel))
            {
                init_param = Expression.Assign(param, root);
                exps.Add(init_param);
            }
            else
            {
                ParameterExpression rootparame = root as ParameterExpression;
                MemberAssignment bind = Expression.Bind(typeof(DynamicModel).GetProperty("FdValue"), Expression.Convert(rootparame, typeof(object)));
                MemberInitExpression init = Expression.MemberInit(Expression.New(typeof(DynamicModel)), bind);
                init_param = Expression.Assign(param, init);
                exps.Add(init_param);
            }
            for (int i = 1; i < paths.Length; i++)
            {
                SourcePathModel item = paths[i];
                Expression exppath;
                MethodCallExpression getCall;
                switch (item.PathType)
                {
                    case SourcePathType.Variable:
                        exppath = this.ParentNode.SearchVariable(item.SourcePath);
                        getCall = Expression.Call(param, typeof(DynamicModel).GetMethod("Get", new[] { exppath.Type }), exppath);
                        break;
                    case SourcePathType.Index:
                        exppath = Expression.Constant(Convert.ToInt32(item.SourcePath));
                        getCall = Expression.Call(param, typeof(DynamicModel).GetMethod("Get", new[] { typeof(int) }), exppath);
                        break;
                    case SourcePathType.Attribute:
                        exppath = Expression.Constant(item.SourcePath);
                        getCall = Expression.Call(param, typeof(DynamicModel).GetMethod("Get", new[] { typeof(string) }), exppath);
                        break;
                    case SourcePathType.SourcePath:
                        getCall = default;
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
        /// <param name="fdValue"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected (Expression value, Expression init) GetConstByFd(FieldType fdType, string fdValue, ParameterExpression param = null)
        {
            switch (fdType)
            {
                case FieldType.Decimal:
                    return (Expression.Constant(Convert.ToDecimal(fdValue)), null);
                case FieldType.String:
                    return (Expression.Constant(fdValue), null);
                case FieldType.Bool:
                    return (Expression.Constant(Convert.ToBoolean(fdValue)), null);
                case FieldType.Char:
                    return (Expression.Constant(Convert.ToChar(fdValue)), null);
                case FieldType.SourcePath:
                    if (param == null)
                        param = Expression.Variable(typeof(DynamicModel));
                    return this.SearchPath(fdValue, param);
                default:
                    throw new Exception($"{fdType}是不受支持的字段类型");
            }
        }
    }
}
