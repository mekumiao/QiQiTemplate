using QiQiTemplate.Filter;
using System;
using System.Collections.Generic;
using System.Text;

namespace QiQiTemplate.Provide
{
    /// <summary>
    /// 过滤器提供类
    /// </summary>
    public class FilterProvide
    {
        private readonly Dictionary<string, IFilter> _filters;
        /// <summary>
        /// 构造
        /// </summary>
        public FilterProvide()
        {
            this._filters = new Dictionary<string, IFilter>(10);
            this.RegisFilter();
        }
        /// <summary>
        /// 注册过滤器
        /// </summary>
        public void RegisFilter<T>()
            where T : IFilter, new()
        {
            var filter = new T();
            this._filters.Add(filter.Name, filter);
        }
        /// <summary>
        /// 根据名称获取过滤器
        /// </summary>
        /// <param name="filterName">过滤器名称</param>
        /// <returns></returns>
        public IFilter GetFilter(string filterName)
        {
            this._filters.TryGetValue(filterName.ToLower(), out var filter);
            return filter;
        }
        private void RegisFilter()
        {
            this.RegisFilter<PadLeftFilter>();
            this.RegisFilter<PadRightFilter>();
            this.RegisFilter<IsNullFilter>();
            this.RegisFilter<JoinFilter>();
        }
    }
}
