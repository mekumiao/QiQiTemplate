using System;
using System.Collections.Generic;
using System.Linq;

namespace QiQiTemplate.Model
{
    /// <summary>
    /// 数据实体
    /// </summary>
    public class DynamicModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string FdName { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public object FdValue { get; set; }
        /// <summary>
        /// 子节点数量
        /// </summary>
        public int Count { get { return this.FdDict.Count; } }

        private readonly Dictionary<string, DynamicModel> FdDict;
        /// <summary>
        /// 构造
        /// </summary>
        public DynamicModel()
        {
            this.FdDict = new Dictionary<string, DynamicModel>(10);
        }

        private DynamicModel Get(object key)
        {
            if (key is string fdName)
            {
                if (FdDict.TryGetValue(fdName, out var result))
                {
                    return result;
                }
                else
                {
                    throw new Exception($"对象没有{fdName}属性");
                }
            }
            else if (key is int idx)
            {
                var result = FdDict.Values.ToArray()[idx];
                return result;
            }
            else
            {
                throw new Exception($"{key}是不受支持的键类型");
            }
        }

        /// <summary>
        /// 根据名称获取子节点
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public DynamicModel Get(string key)
        {
            object prm = key;
            return Get(prm);
        }

        /// <summary>
        /// 根据索引获取子节点
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public DynamicModel Get(int idx)
        {
            object prm = idx;
            return Get(prm);
        }

        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="model"></param>
        public void Set(DynamicModel model)
        {
            FdDict.Remove(model.FdName);
            FdDict.TryAdd(model.FdName, model);
        }

        /// <summary>
        /// 将值转为String
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return FdValue?.ToString();
        }
        /// <summary>
        /// 重载操作符
        /// </summary>
        /// <param name="field1"></param>
        /// <param name="field2"></param>
        /// <returns></returns>
        public static bool operator >(DynamicModel field1, DynamicModel field2) => Convert.ToDecimal(field1.FdValue) > Convert.ToDecimal(field2.FdValue);
        /// <summary>
        /// 重载操作符
        /// </summary>
        /// <param name="field1"></param>
        /// <param name="field2"></param>
        /// <returns></returns>
        public static bool operator <(DynamicModel field1, DynamicModel field2) => Convert.ToDecimal(field1.FdValue) < Convert.ToDecimal(field2.FdValue);
        /// <summary>
        /// 重载操作符
        /// </summary>
        /// <param name="field1"></param>
        /// <param name="field2"></param>
        /// <returns></returns>
        public static bool operator >=(DynamicModel field1, DynamicModel field2) => Convert.ToDecimal(field1.FdValue) >= Convert.ToDecimal(field2.FdValue);
        /// <summary>
        /// 重载操作符
        /// </summary>
        /// <param name="field1"></param>
        /// <param name="field2"></param>
        /// <returns></returns>
        public static bool operator <=(DynamicModel field1, DynamicModel field2) => Convert.ToDecimal(field1.FdValue) <= Convert.ToDecimal(field2.FdValue);
        /// <summary>
        /// 重载操作符
        /// </summary>
        /// <param name="field1"></param>
        /// <param name="field2"></param>
        /// <returns></returns>
        public static bool operator ==(DynamicModel field1, DynamicModel field2) => field1.FdValue.ToString() == field2.FdValue.ToString();
        /// <summary>
        /// 重载操作符
        /// </summary>
        /// <param name="field1"></param>
        /// <param name="field2"></param>
        /// <returns></returns>
        public static bool operator !=(DynamicModel field1, DynamicModel field2) => field1.FdValue.ToString() != field2.FdValue.ToString();
        /// <summary>
        /// 重载操作符
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => this.FdValue.ToString().GetHashCode();
        /// <summary>
        /// 重载操作符
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is DynamicModel fd)
            {
                var str1 = this.FdValue.ToString();
                var str2 = fd.FdValue.ToString();
                return str1 == str2;
            }
            return false;
        }
    }
}
