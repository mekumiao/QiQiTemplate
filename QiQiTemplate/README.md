# tempdemo

#### 介绍
基于lambda表达式的模板代码工具

~~~html

{{#if}}

{{#else if}}

{{#else}}

{{#/if}}

{{#each data val idx}}

{{#/each}}

{{#each _data val}}

{{#/each}}

{{_data.val}}

{{#define int num = 10}}
{{#define string msg = "success"}}
{{#define decimal price = 12.25}}


{{each _data.usings val key}}
using {{val}};
{{/each}}

namespace {{_data.namespace}}
{
    [DataContract]
    public class {{_data.class}} : ModelBase
    {
        {{each _data.fields val key}}
        [DataMember]
        {{if key == 0}}
        [Key]
        {{/if}}
        {{if val.type == "byte[]"}}
        [Timestamp]
        {{/if}}
        public {{val.type}} {{val.name}} { get; set; }
        
        {{/each}}
    }
}

字符串解析为表达式树

1. if 语句解析
{{#if x + y >= 10 & "xxx" != str}}

2.each 
{{#each _data val idx}}

{{#each _data val}}

3.print
{{_data.val}}

4.define
{{#define int num = 0}}


{{#each _data.usings val key}}
using {{val}};
{{#/each}}

namespace {{_data.namespace}}
{
    [DataContract]
    public class {{_data.class}} : ModelBase
    {
        {{#each _data.fields val key}}
        [DataMember]
        {{#if key == 0}}
        [Key]
        {{#/if}}
        {{#if val.type == "byte[]"}}
        [Timestamp]
        {{#/if}}
        public {{val.type}} {{val.name}} { get; set; }
        
        {{#/each}}
    }
}

{{#define int num i = 10}}

{{#if i == 0}}
{{_data.val}}
{{#else if i == 2}}
{{_data.val}}
{{#else}}
{{_data.val}}
{{#/if}}
~~~