using System.IO;
using System.Text;

namespace QiQiTemplate.Provide
{
    public class OutPutProvide
    {
        private readonly StringBuilder _stringBuilder;

        public OutPutProvide()
        {
            this._stringBuilder = new StringBuilder();
        }

        public OutPutProvide(StringBuilder builder)
        {
            this._stringBuilder = builder;
        }

        public void Print(object code)
        {
            this._stringBuilder.Append(code);
        }

        public void PrintLine(object code)
        {
            this._stringBuilder.AppendLine($"{code}");
        }

        public void PrintLine()
        {
            this._stringBuilder.AppendLine();
        }

        public string GetCode()
        {
            return this._stringBuilder.ToString().TrimEnd();
        }

        public void Clear()
        {
            this._stringBuilder.Clear();
        }

        public void OutPut(string path)
        {
            using var writer = new StreamWriter(path);
            writer.Write(this.GetCode());
        }
    }
}
