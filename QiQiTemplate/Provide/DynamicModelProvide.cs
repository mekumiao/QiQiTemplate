using QiQiTemplate.Enums;
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
        /// 从json文件加载数据
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public DynamicModel CreateByFilePath(string path)
        {
            using var reader = new StreamReader(path);
            return CreateByJson(reader.ReadToEnd());
        }
        /// <summary>
        /// 从json字符串加载数据
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public DynamicModel CreateByJson(string json)
        {
            DynamicModel model;
            using var doc = JsonDocument.Parse(json);
            switch (doc.RootElement.ValueKind)
            {
                case JsonValueKind.Array:
                    model = new DynamicModel(FieldType.Array)
                    {
                        FdName = "_data"
                    };
                    CreateByJson(doc.RootElement.EnumerateArray(), model);
                    break;
                case JsonValueKind.Object:
                    model = new DynamicModel(FieldType.Object)
                    {
                        FdName = "_data"
                    };
                    CreateByJson(doc.RootElement.EnumerateObject(), model);
                    break;
                default:
                    throw new Exception("暂不支持除 array object 类型以外的类型");
            }
            return model;
        }

        private void CreateByJson(ObjectEnumerator obj, DynamicModel parent)
        {
            foreach (var item in obj)
            {
                DynamicModel model;
                switch (item.Value.ValueKind)
                {
                    case JsonValueKind.Undefined:
                        model = new DynamicModel(FieldType.String)
                        {
                            FdName = item.Name,
                            FdValue = item.Value.GetString()
                        };
                        parent.Set(model);
                        break;
                    case JsonValueKind.Object:
                        model = new DynamicModel(FieldType.Object)
                        {
                            FdName = item.Name
                        };
                        CreateByJson(item.Value.EnumerateObject(), model);
                        parent.Set(model);
                        break;
                    case JsonValueKind.Array:
                        model = new DynamicModel(FieldType.Array)
                        {
                            FdName = item.Name
                        };
                        CreateByJson(item.Value.EnumerateArray(), model);
                        parent.Set(model);
                        break;
                    case JsonValueKind.String:
                        model = new DynamicModel(FieldType.String)
                        {
                            FdName = item.Name,
                            FdValue = item.Value.GetString()
                        };
                        parent.Set(model);
                        break;
                    case JsonValueKind.Number:
                        model = new DynamicModel(FieldType.Decimal)
                        {
                            FdName = item.Name,
                            FdValue = item.Value.GetInt32()
                        };
                        parent.Set(model);
                        break;
                    case JsonValueKind.True:
                        model = new DynamicModel(FieldType.Bool)
                        {
                            FdName = item.Name,
                            FdValue = item.Value.GetBoolean()
                        };
                        parent.Set(model);
                        break;
                    case JsonValueKind.False:
                        model = new DynamicModel(FieldType.Bool)
                        {
                            FdName = item.Name,
                            FdValue = item.Value.GetBoolean()
                        };
                        parent.Set(model);
                        break;
                    case JsonValueKind.Null:
                        model = new DynamicModel(FieldType.String)
                        {
                            FdName = item.Name,
                            FdValue = null
                        };
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
                DynamicModel model;
                switch (item.ValueKind)
                {
                    case JsonValueKind.Undefined:
                        model = new DynamicModel(FieldType.String)
                        {
                            FdName = idx.ToString(),
                            FdValue = item.GetString()
                        };
                        parent.Set(model);
                        break;
                    case JsonValueKind.Object:
                        model = new DynamicModel(FieldType.Object)
                        {
                            FdName = idx.ToString()
                        };
                        CreateByJson(item.EnumerateObject(), model);
                        parent.Set(model);
                        break;
                    case JsonValueKind.Array:
                        model = new DynamicModel(FieldType.Array)
                        {
                            FdName = idx.ToString()
                        };
                        CreateByJson(item.EnumerateArray(), model);
                        parent.Set(model);
                        break;
                    case JsonValueKind.String:
                        model = new DynamicModel(FieldType.String)
                        {
                            FdName = idx.ToString(),
                            FdValue = item.GetString()
                        };
                        parent.Set(model);
                        break;
                    case JsonValueKind.Number:
                        model = new DynamicModel(FieldType.Decimal)
                        {
                            FdName = idx.ToString(),
                            FdValue = item.GetInt32()
                        };
                        parent.Set(model);
                        break;
                    case JsonValueKind.True:
                        model = new DynamicModel(FieldType.Bool)
                        {
                            FdName = idx.ToString(),
                            FdValue = item.GetBoolean()
                        };
                        parent.Set(model);
                        break;
                    case JsonValueKind.False:
                        model = new DynamicModel(FieldType.Bool)
                        {
                            FdName = idx.ToString(),
                            FdValue = item.GetBoolean()
                        };
                        parent.Set(model);
                        break;
                    case JsonValueKind.Null:
                        model = new DynamicModel(FieldType.String)
                        {
                            FdName = idx.ToString(),
                            FdValue = null
                        };
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