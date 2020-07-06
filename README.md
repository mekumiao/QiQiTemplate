

# QiQiTemplate

#### 介绍
基于lambda表达式的模板代码工具

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
{{#else if 2 > 3}}
我是一只猪.
{{#/else if}}
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

### 运算

> 下个版本

~~~html
{{#oper num += 1}}
{{#oper num -= 2}}
{{#oper num *= num}}
{{#oper num /= 3}}
~~~

### 三元表达式

> 下个版本

~~~html
{{#cond num > 2 ? num : "..."}}
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

### 输出 {{ 与 }}

> 使用 \ 来进行转义

~~~html
{{#each _data.names val idx}}
\{\{ 我是{{val}} \}\}
{{#/each}}

//将输出 {{ 我是wyl }}
~~~



## 调用

~~~c#
var ndProvide = new NodeContextProvide();
var cdProvide = new CoderExpressionProvide();
var dyProvide = new DynamicModelProvide();

//加载数据
var model = dyProvide.CreateByFilePath(@"Temp.json");
//编译模板
var action = ndProvide.BuildTemplateByPath(@"Temp.txt", cdProvide).Compile();
//执行
action.Invoke(model);
//输出
Console.WriteLine(cdProvide.GetCode());
~~~

