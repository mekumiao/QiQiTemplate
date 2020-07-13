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
    public class SETNodeContext : NodeContext
    {
        /// <summary>
        /// 正则
        /// </summary>
        protected static readonly Regex ParsingRegex = new Regex(@"{{#set\s(?<name>[a-zA-Z_][\w]+)\s=\s(?<value>.+)}}", RegexOptions.Compiled);
        /// <summary>
        /// 正则
        /// </summary>
        protected static readonly Regex RegexValue = new Regex("(?<=\")(.*)(?=\")", RegexOptions.Compiled);
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
        public SETNodeContext(string code, NodeBlockContext parent, OutPutProvide output)
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
            this.Model = new DeFineModel
            {
                FdType = TypeHelper.GetFieldTypeByValue(mth.Groups["value"].Value.Trim()),
                ArgName = mth.Groups["name"].Value.Trim().Replace("\"", ""),
                ArgValue = mth.Groups["value"].Value.Trim(),
            };
            if (this.Model.FdType == FieldType.String)
            {
                this.Model.ArgValue = RegexValue.Match(this.Model.ArgValue).Value;
            }
        }
        /// <summary>
        /// 转换表达式
        /// </summary>
        public override void ConvertToExpression()
        {
            //将变量表达式加入到作用域中
            if (this.ParentNode is NodeBlockContext block)
            {
                block.SearchVariable(this.Model.ArgName);
                if (!block.Scope.TryGetValue(this.Model.ArgName, out var expression))
                {
                    ParameterExpression param;
                    if (this.Model.FdType == FieldType.SourcePath)
                    {
                        param = Expression.Variable(typeof(DynamicModel), this.Model.ArgName);
                        var (_, init) = this.SearchPath(this.Model.ArgValue, param);
                        this.NdExpression = init;
                    }
                    else
                    {
                        var (value, _) = this.GetConstByFd(this.Model.FdType, this.Model.ArgValue);
                        param = Expression.Variable(value.Type, this.Model.ArgName);
                        block.DefineParams.Add(param);
                        this.NdExpression = Expression.Assign(param, value);
                    }
                    block.Scope.Add(this.Model.ArgName, param);
                }
                else
                {
                    if (this.Model.FdType == FieldType.SourcePath)
                    {
                        var (_, init) = this.SearchPath(this.Model.ArgValue, expression as ParameterExpression);
                        this.NdExpression = init;
                    }
                    else
                    {
                        var (value, _) = this.GetConstByFd(this.Model.FdType, this.Model.ArgValue);
                        if (this.ParentNode is EACHNodeContext each)
                        {
                            each.EachVars.Add(Expression.Assign(expression, value));
                        }
                        else
                        {
                            this.NdExpression = Expression.Assign(expression, value);
                        }
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
