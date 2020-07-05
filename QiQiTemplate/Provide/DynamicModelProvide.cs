using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using static System.Text.Json.JsonElement;

namespace QiQiTemplate
{
    public class DynamicModelProvide
    {
        public DynamicModel CreateByJson(string json)
        {
            var model = new DynamicModel();
            using var doc = JsonDocument.Parse(json);
            var kind = doc.RootElement.ValueKind;
            if (kind == JsonValueKind.Array)
            {
                model.FdName = "_data";
                LoadByJson(doc.RootElement.EnumerateArray(), model);
            }
            else if (kind == JsonValueKind.Object)
            {
                model.FdName = "_data";
                LoadByJson(doc.RootElement.EnumerateObject(), model);
            }
            else
            {
                throw new Exception("暂不支持除 array object 类型以外的类型");
            }
            return model;
        }

        public DynamicModel CreateByFilePath(string path)
        {
            using var reader = new StreamReader(path);
            return CreateByJson(reader.ReadToEnd());
        }

        private void LoadByJson(ObjectEnumerator obj, DynamicModel parent)
        {
            foreach (var item in obj)
            {
                var model = new DynamicModel();
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

        private void LoadByJson(ArrayEnumerator arr, DynamicModel parent)
        {
            int idx = 0;
            foreach (var item in arr)
            {
                var model = new DynamicModel();
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

    }
}
