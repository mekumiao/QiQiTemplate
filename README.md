

# QiQiTemplate

#### 介绍
基于lambda表达式树实现的轻量级模板工具

## 安装

> Install-Package QiQiTemplate

## 语法

> 注意: 下列语法都有严格的空格要求
>
> 因为作者以及懒成猪了,所以没有做适配

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

### set 定义\赋值

> 如果作用域中不存在变量,则声明变量并且赋值

~~~html
{{#set index = 1}}
~~~

> 小技巧

~~~html
{{#set str = """}}
{{str}} // 将输出 "
~~~

### oper 运算符

> 仅支持 ++ 和 --
>
> 注: 作者已经懒成猪了

~~~html
{{#oper num++}}
{{#oper num--}}
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

### 完整例子

> 模板 => temp.txt

~~~html
{{#set index = 1}}
{{#each _data.people val idx}}
    {{#if val.age > 0 & val.age <= 10}}
    {{idx}}.{{val.name}} 儿童 {{val.age}}
    {{#/if}}
    {{#elseif val.age > 10 & val.age < 25}}
    {{idx}}.{{val.name}} 青少年 {{val.age}}
    {{#/elseif}}
    {{#else}}
    {{idx}}.{{val.name}} 中年 {{val.age}}
    {{#/else}}
    {{#each val.tel tl idx2}}
        {{index}}.{{tl}}
        {{#oper index++}}
    {{#/each}}
{{#/each}}
~~~

> json => temp.json

~~~json
{
  "people": [
    {
      "name": "wyl",
      "age": 10,
      "sex": "男",
      "tel": [
        "xxx",
        "xxx",
        "xxx"
      ]
    },
    {
      "name": "wyl",
      "age": 2,
      "sex": "男",
      "tel": [
        "xxx",
        "xxx",
        "xxx"
      ]
    },
    {
      "name": "wyl",
      "age": 30,
      "sex": "男",
      "tel": [
        "xxx",
        "xxx",
        "xxx"
      ]
    },
    {
      "name": "wyl",
      "age": 22,
      "sex": "男",
      "tel": [
        "xxx",
        "xxx",
        "xxx"
      ]
    }
  ]
}
~~~

### 另外

> 项目源码中有完整案例可以参考
>
> 作者是个小菜鸟

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

