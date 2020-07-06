using QiQiTemplate.Enum;
using QiQiTemplate.Provide;

namespace QiQiTemplate.Context
{
    public class ELSEIFNodeContext : IFNodeContext
    {
        public ELSEIFNodeContext(string code, NodeBlockContext parent, OutPutProvide output)
            : base(code, parent, output)
        {
            this.NdType = NodeType.ELSEIF;
        }

        protected override string FormatCode()
        {
            return this.CodeString.Trim().Replace("{{#else if", "");
        }
    }
}
