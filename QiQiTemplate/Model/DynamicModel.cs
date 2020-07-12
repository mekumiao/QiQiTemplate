﻿using System;
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
        /// <summary>
        /// 值类型
        /// </summary>
        public Type ValueType { get { return this.FdValue.GetType(); } }

        private readonly Dictionary<string, DynamicModel> FdDict;
        /// <summary>
        /// 构造
        /// </summary>
        public DynamicModel()
        {
            this.FdDict = new Dictionary<string, DynamicModel>(10);
        }

        /// <summary>
        /// 根据名称获取子节点
        /// </summary>
        /// <param name="fdName"></param>
        /// <returns></returns>
        public DynamicModel Get(string fdName)
        {
            if (FdDict.TryGetValue(fdName, out var result))
            {
                return result;
            }
            throw new Exception($"对象没有{fdName}属性");
        }

        /// <summary>
        /// 根据索引获取子节点
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public DynamicModel Get(int idx)
        {
            if (idx > FdDict.Count - 1) throw new Exception("索引超出界限");
            return FdDict.Values.ToArray()[idx];
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
        public static bool operator >(decimal field1, DynamicModel field2) => field1 > Convert.ToDecimal(field2.FdValue);
        /// <summary>
        /// 重载操作符
        /// </summary>
        /// <param name="field1"></param>
        /// <param name="field2"></param>
        /// <returns></returns>
        public static bool operator <(decimal field1, DynamicModel field2) => field1 < Convert.ToDecimal(field2.FdValue);
        /// <summary>
        /// 重载操作符
        /// </summary>
        /// <param name="field1"></param>
        /// <param name="field2"></param>
        /// <returns></returns>
        public static bool operator >(DynamicModel field1, decimal field2) => Convert.ToDecimal(field1.FdValue) > field2;
        /// <summary>
        /// 重载操作符
        /// </summary>
        /// <param name="field1"></param>
        /// <param name="field2"></param>
        /// <returns></returns>
        public static bool operator <(DynamicModel field1, decimal field2) => Convert.ToDecimal(field1.FdValue) < field2;
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
        public static bool operator <=(decimal field1, DynamicModel field2) => field1 <= Convert.ToDecimal(field2.FdValue);
        /// <summary>
        /// 重载操作符
        /// </summary>
        /// <param name="field1"></param>
        /// <param name="field2"></param>
        /// <returns></returns>
        public static bool operator >=(decimal field1, DynamicModel field2) => field1 >= Convert.ToDecimal(field2.FdValue);
        /// <summary>
        /// 重载操作符
        /// </summary>
        /// <param name="field1"></param>
        /// <param name="field2"></param>
        /// <returns></returns>
        public static bool operator <=(DynamicModel field1, decimal field2) => Convert.ToDecimal(field1.FdValue) <= field2;
        /// <summary>
        /// 重载操作符
        /// </summary>
        /// <param name="field1"></param>
        /// <param name="field2"></param>
        /// <returns></returns>
        public static bool operator >=(DynamicModel field1, decimal field2) => Convert.ToDecimal(field1.FdValue) >= field2;
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
        /// <param name="field1"></param>
        /// <param name="field2"></param>
        /// <returns></returns>
        public static bool operator ==(decimal field1, DynamicModel field2) => field1.ToString() == field2.FdValue.ToString();
        /// <summary>
        /// 重载操作符
        /// </summary>
        /// <param name="field1"></param>
        /// <param name="field2"></param>
        /// <returns></returns>
        public static bool operator !=(decimal field1, DynamicModel field2) => field1.ToString() != field2.FdValue.ToString();
        /// <summary>
        /// 重载操作符
        /// </summary>
        /// <param name="field1"></param>
        /// <param name="field2"></param>
        /// <returns></returns>
        public static bool operator ==(DynamicModel field1, decimal field2) => field1.FdValue.ToString() == field2.ToString();
        /// <summary>
        /// 重载操作符
        /// </summary>
        /// <param name="field1"></param>
        /// <param name="field2"></param>
        /// <returns></returns>
        public static bool operator !=(DynamicModel field1, decimal field2) => field1.FdValue.ToString() != field2.ToString();
        /// <summary>
        /// 重载操作符
        /// </summary>
        /// <param name="field1"></param>
        /// <param name="field2"></param>
        /// <returns></returns>
        public static bool operator ==(string field1, DynamicModel field2) => field1.ToString() == field2.FdValue.ToString();
        /// <summary>
        /// 重载操作符
        /// </summary>
        /// <param name="field1"></param>
        /// <param name="field2"></param>
        /// <returns></returns>
        public static bool operator !=(string field1, DynamicModel field2) => field1.ToString() != field2.FdValue.ToString();
        /// <summary>
        /// 重载操作符
        /// </summary>
        /// <param name="field1"></param>
        /// <param name="field2"></param>
        /// <returns></returns>
        public static bool operator ==(DynamicModel field1, string field2) => field1.FdValue.ToString() == field2.ToString();
        /// <summary>
        /// 重载操作符
        /// </summary>
        /// <param name="field1"></param>
        /// <param name="field2"></param>
        /// <returns></returns>
        public static bool operator !=(DynamicModel field1, string field2) => field1.FdValue.ToString() != field2.ToString();
        /// <summary>
        /// 重载操作符
        /// </summary>
        /// <param name="field1"></param>
        /// <param name="field2"></param>
        /// <returns></returns>
        public static bool operator ==(DynamicModel field1, bool field2) => field1.FdValue.ToString() == field2.ToString();
        /// <summary>
        /// 重载操作符
        /// </summary>
        /// <param name="field1"></param>
        /// <param name="field2"></param>
        /// <returns></returns>
        public static bool operator !=(DynamicModel field1, bool field2) => field1.FdValue.ToString() != field2.ToString();
        /// <summary>
        /// 重载操作符
        /// </summary>
        /// <param name="field1"></param>
        /// <param name="field2"></param>
        /// <returns></returns>
        public static bool operator ==(bool field1, DynamicModel field2) => field1.ToString() == field2.FdValue.ToString();
        /// <summary>
        /// 重载操作符
        /// </summary>
        /// <param name="field1"></param>
        /// <param name="field2"></param>
        /// <returns></returns>
        public static bool operator !=(bool field1, DynamicModel field2) => field1.ToString() != field2.FdValue.ToString();
        /// <summary>
        /// 重载操作符
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static DynamicModel operator ++(DynamicModel field)
        {
            field.FdValue = Convert.ToDecimal(field.FdValue) + 1;
            return field;
        }
        /// <summary>
        /// 重载操作符
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public static DynamicModel operator --(DynamicModel field)
        {
            field.FdValue = Convert.ToDecimal(field.FdValue) - 1;
            return field;
        }
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
