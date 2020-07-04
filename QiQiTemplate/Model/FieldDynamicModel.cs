using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using static System.Text.Json.JsonElement;

namespace QiQiTemplate
{
    public class FieldDynamicModel
    {
        public string FdName { get; set; }
        public object FdValue { get; set; }
        public List<FieldDynamicModel> ChildModel { get { return FdDict.Values.ToList(); } }
        public int Count { get { return this.FdDict.Count; } }

        private readonly Dictionary<string, FieldDynamicModel> FdDict;

        public FieldDynamicModel()
        {
            this.FdDict = new Dictionary<string, FieldDynamicModel>(10);
        }

        public FieldDynamicModel(string json)
            : this()
        {
            this.LoadByJson(json);
        }

        public void LoadByJson(string json)
        {
            using var doc = JsonDocument.Parse(json);
            var kind = doc.RootElement.ValueKind;
            if (kind == JsonValueKind.Array)
            {
                this.FdName = "_data";
                LoadByJson(doc.RootElement.EnumerateArray(), this);
            }
            else if (kind == JsonValueKind.Object)
            {
                this.FdName = "_data";
                LoadByJson(doc.RootElement.EnumerateObject(), this);
            }
            else
            {
                throw new Exception("暂不支持除 array object 类型以外的类型");
            }
        }

        public void LoadByPath(string path)
        {
            using var reader = new StreamReader(path);
            LoadByJson(reader.ReadToEnd());
        }

        public FieldDynamicModel Get(object key)
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

        public FieldDynamicModel Get(string key)
        {
            object prm = key;
            return Get(prm);
        }

        public FieldDynamicModel Get(int idx)
        {
            object prm = idx;
            return Get(prm);
        }

        public void Set(FieldDynamicModel model)
        {
            FdDict.Remove(model.FdName);
            FdDict.TryAdd(model.FdName, model);
        }

        private void LoadByJson(ObjectEnumerator obj, FieldDynamicModel parent)
        {
            foreach (var item in obj)
            {
                var model = new FieldDynamicModel();
                switch (item.Value.ValueKind)
                {
                    case JsonValueKind.Undefined:
                        model.FdValue = item.Name;
                        model.FdValue = item.Value.GetString();
                        parent.Set(model);
                        break;
                    case JsonValueKind.Object:
                        model.FdName = item.Name;
                        LoadByJson(item.Value.EnumerateObject(), model);
                        parent.Set(model);
                        break;
                    case JsonValueKind.Array:
                        model.FdName = item.Name;
                        LoadByJson(item.Value.EnumerateArray(), model);
                        parent.Set(model);
                        break;
                    case JsonValueKind.String:
                        model.FdName = item.Name;
                        model.FdValue = item.Value.GetString();
                        parent.Set(model);
                        break;
                    case JsonValueKind.Number:
                        model.FdName = item.Name;
                        model.FdValue = item.Value.GetInt32();
                        parent.Set(model);
                        break;
                    case JsonValueKind.True:
                        model.FdName = item.Name;
                        model.FdValue = item.Value.GetBoolean();
                        parent.Set(model);
                        break;
                    case JsonValueKind.False:
                        model.FdName = item.Name;
                        model.FdValue = item.Value.GetBoolean();
                        parent.Set(model);
                        break;
                    case JsonValueKind.Null:
                        model.FdValue = item.Name;
                        model.FdValue = null;
                        parent.Set(model);
                        break;
                    default:
                        break;
                }
            }
        }

        private void LoadByJson(ArrayEnumerator arr, FieldDynamicModel parent)
        {
            int idx = 0;
            foreach (var item in arr)
            {
                var model = new FieldDynamicModel();
                switch (item.ValueKind)
                {
                    case JsonValueKind.Undefined:
                        model.FdName = idx.ToString();
                        model.FdValue = item.GetString();
                        parent.Set(model);
                        break;
                    case JsonValueKind.Object:
                        model.FdName = idx.ToString();
                        LoadByJson(item.EnumerateObject(), model);
                        parent.Set(model);
                        break;
                    case JsonValueKind.Array:
                        model.FdName = idx.ToString();
                        LoadByJson(item.EnumerateArray(), model);
                        parent.Set(model);
                        break;
                    case JsonValueKind.String:
                        model.FdName = idx.ToString();
                        model.FdValue = item.GetString();
                        parent.Set(model);
                        break;
                    case JsonValueKind.Number:
                        model.FdName = idx.ToString();
                        model.FdValue = item.GetInt32();
                        parent.Set(model);
                        break;
                    case JsonValueKind.True:
                        model.FdName = idx.ToString();
                        model.FdValue = item.GetBoolean();
                        parent.Set(model);
                        break;
                    case JsonValueKind.False:
                        model.FdName = idx.ToString();
                        model.FdValue = item.GetBoolean();
                        parent.Set(model);
                        break;
                    case JsonValueKind.Null:
                        model.FdName = idx.ToString();
                        model.FdValue = null;
                        parent.Set(model);
                        break;
                    default:
                        break;
                }
                idx++;
            }
        }

        public override string ToString()
        {
            return FdValue?.ToString();
        }
    }
}
