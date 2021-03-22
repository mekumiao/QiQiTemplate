using QiQiTemplate.Enums;
using QiQiTemplate.Model;
using QiQiTemplate.Provide;
using QiQiTemplate.Tool;
using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace QiQiTemplate.Context
{
    /// <summary>
    /// 定义变量节点
    /// </summary>
    public class SETNodeContext : NodeContext
    {
        /// <summary>
        /// 正则
        /// </summary>
        private static readonly Regex ParsingRegex = new Regex(@"{{#set\s(?<name>[a-zA-Z_][\w]+)\s=\s(?<value>.+)}}", RegexOptions.Compiled);
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="code"></param>
        /// <param name="parent"></param>
        /// <param name="output"></param>
        public SETNodeContext(string code, NodeBlockContext parent, OutPutProvide output)
            : base(code, parent, output)
        {
            NdType = NodeType.SET;
        }
        /// <summary>
        /// 信息
        /// </summary>
        public DeFineModel? Model { get; private set; }
        /// <summary>
        /// 转换表达式
        /// </summary>
        public override void ConvertToExpression()
        {
            if (Model == default) throw new ArgumentNullException("未能解析到set变量信息");
            if (ParentNode is NodeBlockContext block)
            {
                if (block.TrySearchVariable(Model.ArgName, out var paramExpression))
                {
                    if (Model.FdType == FieldType.SourcePath)
                    {
                        var (_, init) = SearchPath(Model.ArgValue, paramExpression);
                        NdExpression = init;
                    }
                    else
                    {
                        var value = CreateConstExpression(Model.FdType, Model.ArgValue);
                        NdExpression = Expression.Assign(paramExpression, ConvertToDynamicModel(value));
                    }
                }
                else
                {
                    //将变量表达式加入到作用域中
                    if (Model.FdType == FieldType.SourcePath)
                    {
                        ParameterExpression param = Expression.Variable(typeof(DynamicModel), Model.ArgName);
                        var (_, init) = SearchPath(Model.ArgValue, param);
                        block.DefineParams.Add(param);
                        NdExpression = init;
                        block.Scope.Add(Model.ArgName, param);
                    }
                    else
                    {
                        var value = CreateConstExpression(Model.FdType, Model.ArgValue);
                        ParameterExpression param = Expression.Variable(typeof(DynamicModel), Model.ArgName);
                        block.DefineParams.Add(param);
                        NdExpression = Expression.Assign(param, ConvertToDynamicModel(value));
                        block.Scope.Add(Model.ArgName, param);
                    }
                }
            }
            else
            {
                throw new Exception($"defind节点的父节点必须是块级节点");
            }
        }
        /// <summary>
        /// 解析
        /// </summary>
        protected override void ParsingModel()
        {
            var mth = ParsingRegex.Match(CodeString);
            var value = mth.Groups["value"].Value;
            Model = new DeFineModel
            {
                FdType = TypeHelper.GetFieldTypeByValue(ref value),
                ArgName = mth.Groups["name"].Value,
                ArgValue = value,
            };
        }
    }
}