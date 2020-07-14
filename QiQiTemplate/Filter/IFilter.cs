using System;
using System.Collections.Generic;
using System.Text;

namespace QiQiTemplate.Filter
{
    /// <summary>
    /// 过滤器
    /// </summary>
    public interface IFilter
    {
        /// <summary>
        /// 过滤器名称
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// 过滤抽象方法
        /// </summary>
        /// <param name="code"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public string Filter(object code, object[] args);
    }
}
