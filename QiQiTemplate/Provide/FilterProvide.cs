using QiQiTemplate.Filter;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QiQiTemplate.Provide
{
    /// <summary>
    /// 过滤器提供类
    /// </summary>
    public class FilterProvide
    {
        private static readonly HashSet<Type> FilterTypes = new HashSet<Type>(20);
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
        public static void RegisFilter<T>()
            where T : IFilter, new()
        {
            FilterTypes.Add(typeof(T));
        }
        private static IFilter CreateFilter(Type type)
        {
            return Activator.CreateInstance(type) as IFilter;
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
        /// <summary>
        /// 重置filter
        /// </summary>
        public void Reset()
        {
            this._filters.Clear();
            FilterTypes.ToList().ForEach(x =>
            {
                var name = x.Name;
                if (name.EndsWith("Filter", StringComparison.CurrentCultureIgnoreCase))
                {
                    name = name.Remove(name.Length - 6, 6).ToLower();
                }
                var obj = CreateFilter(x);
                this._filters.Add(name, obj);
            });
        }
        /// <summary>
        /// 注册内置过滤器
        /// </summary>
        private void RegisFilter()
        {
            RegisFilter<PadLeftFilter>();
            RegisFilter<PadRightFilter>();
            RegisFilter<IsNullFilter>();
            RegisFilter<JoinFilter>();
            RegisFilter<OperFilter>();
            RegisFilter<ToLowerFilter>();
            RegisFilter<ToUpperFilter>();
            RegisFilter<ToUpperCaseFilter>();
            RegisFilter<ThenFilter>();
            RegisFilter<RecorderFilter>();
            this.Reset();
        }
    }
}
