using QiQiTemplate.Enums;
using QiQiTemplate.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace QiQiTemplate.Provide
{
    /// <summary>
    /// 访问路径解析提供类
    /// </summary>
    public class SourcePathProvider
    {
        /// <summary>
        /// 解析访问路径
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <returns></returns>
        public static SourcePathModel[] CreateSourcePath(string sourcePath)
        {
            var builder = new StringBuilder(sourcePath);
            var list = new List<SourcePathModel>();
            MatchPath(builder, list);
            return list.ToArray();
        }

        private static void MatchPath(StringBuilder builder, List<SourcePathModel> list)
        {
            Match mh = Regex.Match(builder.ToString(), @"^(?<value>[a-zA-Z_][\w]*)");
            if (mh.Success)
            {
                MatchFunc(SourcePathType.Variable);//变量
                return;
            }
            mh = Regex.Match(builder.ToString(), @"^[.](?<value>[a-zA-Z_][\w]*)");
            if (mh.Success)
            {
                MatchFunc(SourcePathType.Attribute);//属性
                return;
            }
            mh = Regex.Match(builder.ToString(), @"^[\[](?<value>[\d]+)[\]]");
            if (mh.Success)
            {
                MatchFunc(SourcePathType.Index);//索引
                return;
            }
            mh = Regex.Match(builder.ToString(), "^[\\[]\"(?<value>[\\w]+)\"[\\]]");
            if (mh.Success)
            {
                MatchFunc(SourcePathType.Attribute);//属性
                return;
            }
            mh = Regex.Match(builder.ToString(), @"^[\[](?<value>.+)[\]]");
            if (mh.Success)
            {
                MatchFunc(SourcePathType.SourcePath);//嵌套访问路径
                return;
            }
            throw new Exception($"访问路径{builder}无效");
            void MatchFunc(SourcePathType pathType)
            {
                builder.Remove(mh.Index, mh.Length);
                var md = new SourcePathModel
                {
                    PathType = pathType,
                    SourcePath = mh.Groups["value"].Value,
                };
                if (pathType == SourcePathType.SourcePath) md.ChildSourcePathModel = CreateSourcePath(md.SourcePath);
                list.Add(md);
                if (builder.Length > 0) MatchPath(builder, list);
            }
        }
    }
}