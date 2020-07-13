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
        /// <param name="filter"></param>
        public void RegisFilter(IFilter filter)
        {
            this._filters.Add(filter.Name, filter);
        }
        /// <summary>
        /// 根据名称获取过滤器
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IFilter GetFilter(string name)
        {
            this._filters.TryGetValue(name, out var filter);
            return filter;
        }

        private void RegisFilter()
        {
            this.RegisFilter(new PadLeftFilter());
            this.RegisFilter(new PadRightFilter());
        }
    }
}
