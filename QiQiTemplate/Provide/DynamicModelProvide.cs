using QiQiTemplate.Model;
using System;
using System.IO;
using System.Text.Json;
using static System.Text.Json.JsonElement;

namespace QiQiTemplate.Provide
{
    /// <summary>
    /// 数据提供类
    /// </summary>
    public class DynamicModelProvide
    {
        /// <summary>
        /// 从json字符串加载数据
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public DynamicModel CreateByJson(string json)
        {
            var model = new DynamicModel();
            using var doc = JsonDocument.Parse(json);
            var kind = doc.RootElement.ValueKind;
            if (kind == JsonValueKind.Array)
            {
                model.FdName = "_data";
                CreateByJson(doc.RootElement.EnumerateArray(), model);
            }
            else if (kind == JsonValueKind.Object)
            {
                model.FdName = "_data";
                CreateByJson(doc.RootElement.EnumerateObject(), model);
            }
            else
            {
                throw new Exception("暂不支持除 array object 类型以外的类型");
            }
            return model;
        }

        /// <summary>
        /// 从json文件加载数据
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public DynamicModel CreateByFilePath(string path)
        {
            using var reader = new StreamReader(path);
            return CreateByJson(reader.ReadToEnd());
        }

        private void CreateByJson(ObjectEnumerator obj, DynamicModel parent)
        {
            foreach (var item in obj)
            {
                var model = new DynamicModel();
                switch (item.Value.ValueKind)
                {
                    case JsonValueKind.Undefined:
                        model.FdName = item.Name;
                        model.FdValue = item.Value.GetString();
                        parent.Set(model);
                        break;
                    case JsonValueKind.Object:
                        model.FdName = item.Name;
                        CreateByJson(item.Value.EnumerateObject(), model);
                        parent.Set(model);
                        break;
                    case JsonValueKind.Array:
                        model.FdName = item.Name;
                        CreateByJson(item.Value.EnumerateArray(), model);
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
                        model.FdName = item.Name;
                        model.FdValue = null;
                        parent.Set(model);
                        break;
                    default:
                        break;
                }
            }
        }

        private void CreateByJson(ArrayEnumerator arr, DynamicModel parent)
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
                        CreateByJson(item.EnumerateObject(), model);
                        parent.Set(model);
                        break;
                    case JsonValueKind.Array:
                        model.FdName = idx.ToString();
                        CreateByJson(item.EnumerateArray(), model);
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
