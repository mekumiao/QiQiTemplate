

# QiQiTemplate

#### 介绍
基于lambda表达式树实现的轻量级模板工具

## 安装

> Install-Package QiQiTemplate

## 语法

### print 输出

~~~html
{{_data.name}}
~~~

### if 判断

~~~html
{{#if 1 > 2}}
我是一只额.
{{#/if}}
{{#elseif 2 > 3}}
我是一只猪.
{{#/elseif}}
{{#else}}
我可能不是人.
{{#/else}}
~~~

### each 循环

~~~html
{{#each _data.names val idx}}
{{idx}}.我叫{{val}}
{{#/each}}
~~~

### define 变量

> 用法

~~~html
{{#define name = "wyl"}}
{{name}}
~~~

> 小技巧

~~~html
{{#define str = """}}
{{str}} // 将输出 "
~~~

### 赋值

> 仅支持 ++ 和 --
>
> 注: 作者已经懒成猪了

~~~html
{{#set num++}}
{{#set num--}}
~~~

### 根节点

> `_data`

~~~html
{{_data.name}}
{{#define name = _data.name}}
~~~

> json数据示例

~~~json
{
    "name":"wyl",
    "names":["wyl","xxx","yyy"]
}
~~~

### 输出 '{{' 或者 '}}'

> 使用 \ 来进行转义

~~~html
{{#each _data.names val idx}}
\{\{ 我是{{val}} \}\}
{{#/each}}

//将输出 {{ 我是wyl }}
~~~

## 调用

~~~c#
var dyProvide = new DynamicModelProvide();//数据加载类
var outProvide = new OutPutProvide();//输出类
var ndProvide = new NodeContextProvide();//模板编译类

//加载数据
var data = dyProvide.CreateByFilePath(@"Temp.json");
//编译模板
var action = ndProvide.BuildTemplateByPath(@"Temp.txt", outProvide).Compile();
//执行
action.Invoke(data);
//输出到文件
outProvide.OutPut(@"output.txt");
//输出到控制台
Console.Write(outProvide.ToString());
~~~

