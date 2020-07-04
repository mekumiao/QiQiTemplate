using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace QiQiTemplate
{
    public class SourcePathProvider
    {
        public static SourcePathModel[] CreateSourcePath(string sourcePath)
        {
            var builder = new StringBuilder(sourcePath);
            var list = new List<SourcePathModel>();
            MatchPath(builder, list);
            return list.ToArray();
        }

        private static void MatchPath(StringBuilder builder, List<SourcePathModel> list)
        {
            Match mh = Regex.Match(builder.ToString(), @"^[a-zA-Z_][\w]*");
            if (mh.Success)
            {
                MatchFunc(SourcePathType.Variable);//Var
                return;
            }
            mh = Regex.Match(builder.ToString(), @"(?<=[.])[a-zA-Z_][\w]*");
            if (mh.Success)
            {
                MatchFunc(SourcePathType.Attribute);//Attr
                return;
            }
            mh = Regex.Match(builder.ToString(), @"(?<=[\[])([\d]+?)(?=[\]])");
            if (mh.Success)
            {
                MatchFunc(SourcePathType.Index);//Index
                return;
            }
            mh = Regex.Match(builder.ToString(), "(?<=[\\[])(\"[\\w]*?\")(?=[\\]])");
            if (mh.Success)
            {
                MatchFunc(SourcePathType.Attribute);//Attr
                return;
            }
            mh = Regex.Match(builder.ToString(), @"^(?<=[\[])([a-zA-Z_][\w]*?)(?=[\]])");
            if (mh.Success)
            {
                MatchFunc(SourcePathType.Variable);//Var
                return;
            }
            void MatchFunc(SourcePathType pathType)
            {
                builder.Remove(mh.Index, mh.Length);
                list.Add(new SourcePathModel
                {
                    PathType = pathType,
                    SourcePath = mh.Value
                });
                if (builder.Length > 0) MatchPath(builder, list);
            }
        }

    }
}
