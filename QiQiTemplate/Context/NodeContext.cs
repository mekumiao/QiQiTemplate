using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace QiQiTemplate
{
    public abstract class NodeContext
    {
        protected static readonly Regex IsIFRegex = new Regex(@"^\s*{{#if\s+[\s\S]+}}\s*$", RegexOptions.Compiled);//针对if的匹配
        protected static readonly Regex IsELSEIFRegex = new Regex(@"^\s*{{#else\s+if\s+[\s\S]+}}\s*$", RegexOptions.Compiled);//针对else if的匹配
        protected static readonly Regex IsELSERegex = new Regex(@"^\s*{{#else}}\s*$", RegexOptions.Compiled);//针对else的匹配
        protected static readonly Regex IsENDIFRegex = new Regex(@"^\s*{{#/if}}\s*$", RegexOptions.Compiled);//针对if结束的匹配
        protected static readonly Regex IsEACHRegex = new Regex(@"^\s*{{#each\s+((?!{{|}}).)+}}\s*$", RegexOptions.Compiled);//针对each循环的匹配
        protected static readonly Regex IsENDEACHRegex = new Regex(@"^\s*{{#/each}}\s*$", RegexOptions.Compiled);//针对each结束的匹配
        protected static readonly Regex IsPRINTRegex = new Regex("({{[^{](((?!{{|}}).)+)}})+", RegexOptions.Compiled);//针对print的匹配
        protected static readonly Regex IsDEFINERegex = new Regex(@"^\s*{{#define\s+[\w]+[\s\S]+}}\s*$", RegexOptions.Compiled);//针对define的匹配

        protected CorderExpressionProvide CorderProvide;

        public string NodeId { get; }

        public NodeType NdType { get; protected set; }

        public Expression NdExpression { get; protected set; }

        public string CodeString { get; }

        public NodeBlockContext ParentNode { get; }

        public NodeContext(string code, NodeBlockContext parent)
        {
            this.CorderProvide = new CorderExpressionProvide();
            this.NodeId = Guid.NewGuid().ToString("N");
            this.CodeString = code;
            this.ParentNode = parent;
        }

        /// <summary>
        /// 获取代码节点的类型
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static NodeType GetNodeType(string code)
        {
            return code switch
            {
                string msg when IsIFRegex.IsMatch(msg) => NodeType.IF,
                string msg when IsELSEIFRegex.IsMatch(msg) => NodeType.ELSEIF,
                string msg when IsELSERegex.IsMatch(msg) => NodeType.ELSE,
                string msg when IsENDIFRegex.IsMatch(msg) => NodeType.ENDIF,
                string msg when IsEACHRegex.IsMatch(msg) => NodeType.EACH,
                string msg when IsENDEACHRegex.IsMatch(msg) => NodeType.ENDEACH,
                string msg when IsPRINTRegex.IsMatch(msg) => NodeType.PRINT,
                string msg when IsDEFINERegex.IsMatch(msg) => NodeType.DEFINE,
                _ => NodeType.STRING,
            };
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
        /// <returns></returns>
        public BlockExpression SearchPath(ParameterExpression param, string sourcePath)
        {
            var exps = new List<Expression>(10);
            var paths = SourcePathProvider.CreateSourcePath(sourcePath);

            Expression root = this.ParentNode.SearchVariable(paths[0].SourcePath);
            BinaryExpression init_param;
            if (root.Type == typeof(FieldDynamicModel))
            {
                init_param = Expression.Assign(param, root);
                exps.Add(init_param);
            }
            else if (root.Type == typeof(int))
            {
                ParameterExpression idx = root as ParameterExpression;
                MemberAssignment bind1 = Expression.Bind(typeof(FieldDynamicModel).GetProperty("FdName"), Expression.Constant(idx.Name));
                MemberAssignment bind2 = Expression.Bind(typeof(FieldDynamicModel).GetProperty("FdValue"), Expression.Convert(idx, typeof(object)));
                MemberInitExpression init = Expression.MemberInit(Expression.New(typeof(FieldDynamicModel)), bind1, bind2);
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
                        getCall = Expression.Call(param, typeof(FieldDynamicModel).GetMethod("Get", new[] { typeof(int) }), exppath);
                        break;
                    case SourcePathType.Attribute:
                        exppath = Expression.Constant(item.SourcePath);
                        getCall = Expression.Call(param, typeof(FieldDynamicModel).GetMethod("Get", new[] { typeof(string) }), exppath);
                        break;
                    case SourcePathType.Variable:
                        exppath = this.ParentNode.SearchVariable(item.SourcePath);
                        getCall = Expression.Call(param, typeof(FieldDynamicModel).GetMethod("Get", new[] { exppath.Type }), exppath);
                        break;
                    default:
                        throw new Exception($"不支持{item.PathType}类型的访问路径");
                }
                init_param = Expression.Assign(param, getCall);
                exps.Add(init_param);
            }
            return Expression.Block(exps);
        }
    }
}
