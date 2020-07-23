

# QiQiTemplate

#### 介绍
基于lambda表达式树实现的轻量级模板工具

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
> 因为作者以及懒成猪了,所以没有做适配

### print 输出

> 普通输出

~~~html
{{_data.name}}
~~~

> 带过滤器的输出

~~~html
{{_data.name isnull("***")}} //空值过滤器.如果为空则输出 ***
~~~

> 支持的过滤器有
>
> + isnull   空值过滤器
> + join  连接过滤器
> + padleft   左侧补位过滤器
> + padright  右侧补位过滤器
> + toupper   转为大写
> + tolower   转为小写
> + touppercase 首字母大写
> + oper      运算
> + then      类似于三目运算  例如:{{_data.name then("wyl","好人","坏人")}}   如果name = "wyl" 则输出 好人  否则 输出坏人

> 示例:

~~~html
{{_data.age padleft(3,'0')}}// 022
{{_data.tags join(';')}} //程序员;码畜
~~~

> 小技巧
>
> 在使用join过滤器时不能使用 ','作为分隔字符.
>
> 但可以间接的使用,例如:

~~~html
{{#set tag = ','}}
{{_data.tags join(tag)}}
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
{{#set name = _data.name}}
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

### 关于属性的嵌套访问

> 只要是从_data根节点继承下来的对象,都能通过嵌套访问路径进行值的获取
>
> 例如:

~~~html
{{#set index = 2}}
{{#set obj = _data.people[0]}}
{{obj.tel[index]}} //xxx
{{obj["name"]}}//wyl
~~~

> 中括号中可以是索引,属性名称,或者变量

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

