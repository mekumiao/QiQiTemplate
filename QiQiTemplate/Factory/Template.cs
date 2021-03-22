using QiQiTemplate.Model;
using QiQiTemplate.Provide;
using System;

/// <summary>
/// 模板类
/// </summary>
public class Template
{
    private readonly NodeContextProvide contextProvide = new NodeContextProvide();
    private readonly DynamicModelProvide modelProvide = new DynamicModelProvide();
    private readonly OutPutProvide putProvide = new OutPutProvide();
    private readonly string tempCode;
    private Action<DynamicModel>? templateAction;
    /// <summary>
    /// 构建模板
    /// </summary>
    /// <param name="tempcode"></param>
    public Template(string tempcode)
    {
        tempCode = tempcode;
    }
    /// <summary>
    /// 编译模板
    /// </summary>
    public void Build()
    {
        templateAction = contextProvide.Build(tempCode, putProvide).Compile();
    }
    /// <summary>
    /// 执行模板
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public string Invoke(string json)
    {
        var model = modelProvide.CreateByJson(json);
        return Invoke(model);
    }
    /// <summary>
    /// 执行模板
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public string Invoke(DynamicModel model)
    {
        templateAction?.Invoke(model);
        var outstring = putProvide.ToString();
        putProvide.Clear();
        return outstring;
    }
}