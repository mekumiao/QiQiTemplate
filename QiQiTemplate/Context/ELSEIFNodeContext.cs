using QiQiTemplate.Enum;
using QiQiTemplate.Provide;

namespace QiQiTemplate.Context
{
    public class ELSEIFNodeContext : IFNodeContext
    {
        public ELSEIFNodeContext(string code, NodeBlockContext parent, CoderExpressionProvide coder)
            : base(code, parent, coder)
        {
            this.NdType = NodeType.ELSEIF;
        }

        protected override string FormatCode()
        {
            return this.CodeString.Trim().Replace("{{#else if", "");
        }
    }
}
