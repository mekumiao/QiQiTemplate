using System;
using System.Collections.Generic;
using System.Linq;

namespace QiQiTemplate.Model
{
    public class DynamicModel
    {
        public string FdName { get; set; }
        public object FdValue { get; set; }
        public int Count { get { return this.FdDict.Count; } }

        private readonly Dictionary<string, DynamicModel> FdDict;

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

        public DynamicModel Get(string key)
        {
            object prm = key;
            return Get(prm);
        }

        public DynamicModel Get(int idx)
        {
            object prm = idx;
            return Get(prm);
        }

        public void Set(DynamicModel model)
        {
            FdDict.Remove(model.FdName);
            FdDict.TryAdd(model.FdName, model);
        }

        public override string ToString()
        {
            return FdValue?.ToString();
        }

        public static bool operator >(DynamicModel field1, DynamicModel field2)
        {
            var v1 = Convert.ToDecimal(field1.FdValue);
            var v2 = Convert.ToDecimal(field2.FdValue);
            return v1 > v2;
        }
        public static bool operator <(DynamicModel field1, DynamicModel field2)
        {
            var v1 = Convert.ToDecimal(field1.FdValue);
            var v2 = Convert.ToDecimal(field2.FdValue);
            return v1 < v2;
        }

        public static bool operator >=(DynamicModel field1, DynamicModel field2)
        {
            var v1 = Convert.ToDecimal(field1.FdValue);
            var v2 = Convert.ToDecimal(field2.FdValue);
            return v1 >= v2;
        }
        public static bool operator <=(DynamicModel field1, DynamicModel field2)
        {
            var v1 = Convert.ToDecimal(field1.FdValue);
            var v2 = Convert.ToDecimal(field2.FdValue);
            return v1 <= v2;
        }
        public static bool operator ==(DynamicModel field1, DynamicModel field2)
        {
            var v1 = field1.FdValue.ToString();
            var v2 = field2.FdValue.ToString();
            return v1 == v2;
        }
        public static bool operator !=(DynamicModel field1, DynamicModel field2)
        {
            var v1 = field1.FdValue.ToString();
            var v2 = field2.FdValue.ToString();
            return v1 != v2;
        }

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

        public override int GetHashCode()
        {
            var hs = this.FdValue.ToString().GetHashCode();
            return hs;
        }
    }
}
