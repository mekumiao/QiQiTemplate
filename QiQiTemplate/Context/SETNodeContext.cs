using QiQiTemplate.Enum;
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
    public class SetNodeContext : NodeContext
    {
        /// <summary>
        /// 正则
        /// </summary>
        private static readonly Regex ParsingRegex = new Regex(@"{{#set\s(?<name>[a-zA-Z_][\w]+)\s=\s(?<value>.+)}}", RegexOptions.Compiled);
        /// <summary>
        /// 信息
        /// </summary>
        public DeFineModel Model { get; private set; }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="code"></param>
        /// <param name="parent"></param>
        /// <param name="output"></param>
        public SetNodeContext(string code, NodeBlockContext parent, OutPutProvide output)
            : base(code, parent, output)
        {
            this.NdType = NodeType.SET;
        }
        /// <summary>
        /// 解析
        /// </summary>
        protected override void ParsingModel()
        {
            var mth = ParsingRegex.Match(this.CodeString);
            var value = mth.Groups["value"].Value;
            this.Model = new DeFineModel
            {
                FdType = TypeHelper.GetFieldTypeByValue(ref value),
                ArgName = mth.Groups["name"].Value,
                ArgValue = value,
            };
        }
        /// <summary>
        /// 转换表达式
        /// </summary>
        public override void ConvertToExpression()
        {
            if (this.ParentNode is NodeBlockContext block)
            {
                if (block.TrySearchVariable(this.Model.ArgName, out var paramExpression))
                {
                    if (this.Model.FdType == FieldType.SourcePath)
                    {
                        var (_, init) = this.SearchPath(this.Model.ArgValue, paramExpression);
                        this.NdExpression = init;
                    }
                    else
                    {
                        var (value, _) = this.GetConstByFd(this.Model.FdType, this.Model.ArgValue);
                        this.NdExpression = Expression.Assign(paramExpression, this.ConvertToDynamicModel(value));
                    }
                }
                else
                {
                    //将变量表达式加入到作用域中
                    if (this.Model.FdType == FieldType.SourcePath)
                    {
                        ParameterExpression param = Expression.Variable(typeof(DynamicModel), this.Model.ArgName);
                        var (_, init) = this.SearchPath(this.Model.ArgValue, param);
                        block.DefineParams.Add(param);
                        this.NdExpression = init;
                        block.Scope.Add(this.Model.ArgName, param);
                    }
                    else
                    {
                        var (value, _) = this.GetConstByFd(this.Model.FdType, this.Model.ArgValue);
                        ParameterExpression param = Expression.Variable(typeof(DynamicModel), this.Model.ArgName);
                        block.DefineParams.Add(param);
                        this.NdExpression = Expression.Assign(param, this.ConvertToDynamicModel(value));
                        block.Scope.Add(this.Model.ArgName, param);
                    }
                }
            }
            else
            {
                throw new Exception($"defind节点的父节点必须是块级节点");
            }
        }
    }
}
