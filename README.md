# QiQiTemplate

## 介绍

> 基于 lambda 表达式树实现的轻量级模板工具

## 安装

> Install-Package QiQiTemplate

## 语法高亮插件

> vscode 插件:tpl-lang
>
> 文件后缀 .tpl
>
> [tpl-lang](https://gitee.com/qiqigouwo/tpl-lang.git)

## 语法

> 注意: 下列语法都有严格的空格要求
>
> 以后有需要在做适配

### print 输出

> 普通输出

```txt
{{_data.name}}
```

> 带过滤器的输出

```txt
{{_data.name isnull("***")}} //空值过滤器.如果为空则输出 ***
```

| 过滤器名称   | 描述           | 调用示例                                                                                  |
| ------------ | -------------- | ----------------------------------------------------------------------------------------- |
| isnull       | 空值过滤器     | {{_data.tag isnull('-')}}                                                                 |
| join         | 连接过滤器     | {{_data.tags join(';')}} .输出 程序员;码畜                                                |
| padleft      | 左侧补位过滤器 | {{_data.age padleft(3,'0')}} .输出 022                                                    |
| padright     | 右侧补位过滤器 |                                                                                           |
| toupper      | 转为大写       | {{_data.name toupper()}} .输出 WYL                                                        |
| tolower      | 转为小写       | {{_data.tag toupper()}} .输出 wyl                                                         |
| touppercase  | 首字母大写     |                                                                                           |
| oper         | 运算           | {{index oper('+',10)}} .将变量 index 加 10                                                |
| then         | 类似于三目运算 | {{_data.name then("wyl","好人","坏人")}}.</br>如果 name = "wyl" 则输出 好人 否则 输出坏人 |
| topascalcase | 帕斯卡命名     |                                                                                           |

?> 小技巧  
在使用 join 过滤器时不能使用 ','作为分隔字符.  
但可以间接的使用

```txt
{{#set tag = ','}}
{{_data.tags join(tag)}}
```

### if 判断

```txt
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
```

### each 循环

```txt
{{#each _data.names val idx}}
{{idx}}.我叫{{val}}
{{#/each}}
```

### set 定义\赋值

> 如果作用域中不存在变量,则声明变量并且赋值

```txt
{{#set index = 1}}
```

> 小技巧

```txt
{{#set str = """}} {{str}} // 将输出 "
```

### oper 运算符

> 仅支持 ++ 和 --
>
> 注: 以后有需要在扩展

```txt
{{#oper num++}} {{#oper num--}}
```

### 根节点

> `_data`

```txt
{{_data.name}} {{#set name = _data.name}}
```

> json 数据示例

```json
{
  "name": "wyl",
  "names": ["wyl", "xxx", "yyy"]
}
```

### 输出 '{{' 或者 '}}'

> 使用 \ 来进行转义

```txt
{{#each _data.names val idx}}
\{\{ 我是{{val}} \}\}
{{#/each}}

//将输出 {{ 我是wyl }}
```

### 关于属性的嵌套访问

> 只要是从\_data 根节点继承下来的对象,都能通过嵌套访问路径进行值的获取
>
> 例如:

```txt
{{#set index = 2}}
{{#set obj = _data.people[0]}}
{{obj.tel[index]}} //xxx
{{obj["name"]}}//wyl
```

> 中括号中可以是索引,属性名称,或者变量

### 完整例子

> 模板 => temp.txt

```txt
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
```

> json => temp.json

```json
{
  "people": [
    {
      "name": "wyl",
      "age": 10,
      "sex": "男",
      "tel": ["xxx", "xxx", "xxx"]
    },
    {
      "name": "wyl",
      "age": 2,
      "sex": "男",
      "tel": ["xxx", "xxx", "xxx"]
    },
    {
      "name": "wyl",
      "age": 30,
      "sex": "男",
      "tel": ["xxx", "xxx", "xxx"]
    },
    {
      "name": "wyl",
      "age": 22,
      "sex": "男",
      "tel": ["xxx", "xxx", "xxx"]
    }
  ]
}
```

### 另外

> 项目源码中有完整案例可以参考

## 使用方法

### 方法一

```cs
//编译模板
var temp = TemplateFactory.CreateByPath("temp.tpl");
//加载数据
var data = new DynamicModelProvide().CreateByPath("data.json");
//执行模板
var msg = temp.Invoke(data);
//打印结果
Console.WriteLine(msg);
```

### 方法二

```cs
var dataProvide = new DynamicModelProvide();//数据加载类
var outProvide = new OutPutProvide();//输出类
var nodeProvide = new NodeContextProvide();//模板编译类

using var reader = new StreamReader("temp.tpl");
var template = reader.ReadToEnd();
//编译模板
var action = nodeProvide.Build(template, outProvide).Compile();
//加载数据
var data = dataProvide.CreateByPath("data.json");
//执行模板
action.Invoke(data);
//输出到文件
outProvide.OutPut("code.txt");
//打印结果
Console.Write(outProvide.ToString());
```
