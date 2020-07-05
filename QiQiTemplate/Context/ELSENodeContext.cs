using QiQiTemplate.Enum;
using QiQiTemplate.Provide;

namespace QiQiTemplate.Context
{
    public class ELSENodeContext : NodeBlockContext
    {
        public ELSENodeContext(string code, NodeBlockContext parent, CoderExpressionProvide coder)
            : base(code, parent, coder)
        {
            this.NdType = NodeType.ELSE;
        }

        public override void ConvertToExpression()
        {
            this.NdExpression = this.MergeNodes();
        }

        protected override void ParsingModel()
        {
            //不需要实现
        }
    }
}
