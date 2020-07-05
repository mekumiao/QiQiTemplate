namespace QiQiTemplate.Enum
{
    /// <summary>
    /// 节点类型
    /// </summary>
    public enum NodeType : byte
    {
        /// <summary>
        /// {{#if}}
        /// </summary>
        IF,
        /// <summary>
        /// {{#/if}}
        /// </summary>
        ENDIF,
        /// <summary>
        /// {{#else if}}
        /// </summary>
        ELSEIF,
        /// <summary>
        /// {{#/else if}}
        /// </summary>
        ENDELSEIF,
        /// <summary>
        /// //{{#else}}
        /// </summary>
        ELSE,
        /// <summary>
        /// {{#/else}}
        /// </summary>
        ENDELSE,
        /// <summary>
        /// {{#each}}
        /// </summary>
        EACH,
        /// <summary>
        /// {{#/each}}
        /// </summary>
        ENDEACH,
        /// <summary>
        /// print 打印节点
        /// </summary>
        PRINT,
        /// <summary>
        /// define 变量节点
        /// </summary>
        DEFINE,
        /// <summary>
        /// 字符串节点
        /// </summary>
        STRING,
    }
}
