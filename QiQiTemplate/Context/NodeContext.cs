using QiQiTemplate.Enums;
using QiQiTemplate.Model;
using QiQiTemplate.Provide;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

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
        protected virtual void ParsingModel() { }
        /// <summary>
        /// 将节点转为Expression
        /// </summary>
        /// <returns></returns>
        public abstract void ConvertToExpression();
        /// <summary>
        /// 将int,string,bool,char,decimal转为DynamicModel类型的表达式树
        /// </summary>
        /// <param name="value"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        protected Expression ConvertToDynamicModel(Expression value, string name = "")
        {
            if (value.Type == typeof(DynamicModel))
            {
                return value;
            }
            else
            {
                MemberAssignment bindName = Expression.Bind(typeof(DynamicModel).GetProperty("FdName"), Expression.Constant(name));
                MemberAssignment bindValue = Expression.Bind(typeof(DynamicModel).GetProperty("FdValue"), Expression.Convert(value, typeof(object)));
                return Expression.MemberInit(Expression.New(typeof(DynamicModel)), bindName, bindValue);
            }
        }
        /// <summary>
        /// 根据对象访问路径构建表达式
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="param_out"></param>
        /// <returns></returns>
        public (ParameterExpression param, BlockExpression init) SearchPath(string sourcePath, ParameterExpression param_out = null)
        {
            var blockparams = new List<ParameterExpression>(10);
            var inits = new List<Expression>(10);

            var paths = SourcePathProvider.CreateSourcePath(sourcePath);
            if (paths.Length < 1) throw new Exception($"{sourcePath}访问路径不存在,或附近有语法错误");
            if (param_out == null) param_out = Expression.Variable(typeof(DynamicModel));
            var param_root = Expression.Variable(typeof(DynamicModel));
            GetPath(paths, param_root);
            blockparams.Add(param_root);
            inits.Add(Expression.Assign(param_out, param_root));
            return (param_out, Expression.Block(blockparams, inits));

            void GetPath(SourcePathModel[] spt, ParameterExpression param_bk)
            {
                foreach (var item in spt)
                {
                    switch (item.PathType)
                    {
                        case SourcePathType.Variable:
                            Expression exppath = this.ParentNode.SearchVariable(item.SourcePath);
                            Expression getCall = this.ConvertToDynamicModel(exppath);
                            inits.Add(Expression.Assign(param_bk, getCall));
                            break;
                        case SourcePathType.Index:
                            exppath = Expression.Constant(Convert.ToInt32(item.SourcePath));
                            getCall = Expression.Call(param_bk, typeof(DynamicModel).GetMethod("Get", new[] { typeof(int) }), exppath);
                            inits.Add(Expression.Assign(param_bk, getCall));
                            break;
                        case SourcePathType.Attribute:
                            exppath = Expression.Constant(item.SourcePath);
                            getCall = Expression.Call(param_bk, typeof(DynamicModel).GetMethod("Get", new[] { typeof(string) }), exppath);
                            inits.Add(Expression.Assign(param_bk, getCall));
                            break;
                        case SourcePathType.SourcePath:
                            ParameterExpression param_bkc = Expression.Variable(typeof(DynamicModel));
                            blockparams.Add(param_bkc);
                            GetPath(item.ChildSourcePathModel, param_bkc);
                            getCall = Expression.Call(param_bk, typeof(DynamicModel).GetMethod("Get", new[] { param_bkc.Type }), param_bkc);
                            inits.Add(Expression.Assign(param_bk, getCall));
                            break;
                        default:
                            throw new Exception($"不支持{item.PathType}类型的访问路径");
                    }
                }
            }
        }
        /// <summary>
        /// 创建常量表达式
        /// </summary>
        /// <param name="fdType"></param>
        /// <param name="fdValue"></param>
        /// <returns></returns>
        protected Expression CreateConstExpression(FieldType fdType, string fdValue)
        {
            return fdType switch
            {
                FieldType.Int => Expression.Constant(Convert.ToInt32(fdValue)),
                FieldType.Decimal => Expression.Constant(Convert.ToDecimal(fdValue)),
                FieldType.String => Expression.Constant(fdValue),
                FieldType.Bool => Expression.Constant(Convert.ToBoolean(fdValue)),
                FieldType.Char => Expression.Constant(Convert.ToChar(fdValue)),
                _ => throw new Exception($"{fdType}是不受支持的字段类型"),
            };
        }
    }
}
