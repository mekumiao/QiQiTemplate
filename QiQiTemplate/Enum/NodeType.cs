using System;
using System.Collections.Generic;
using System.Text;

namespace QiQiTemplate
{
    /// <summary>
    /// 节点类型
    /// </summary>
    public enum NodeType : byte
    {
        /// <summary>
        /// if 判断节点
        /// </summary>
        IF,
        /// <summary>
        /// else if 判断节点
        /// </summary>
        ELSEIF,
        /// <summary>
        /// //else 判断节点
        /// </summary>
        ELSE,
        /// <summary>
        /// if 结束节点
        /// </summary>
        ENDIF,
        /// <summary>
        /// each 循环节点
        /// </summary>
        EACH,
        /// <summary>
        /// each 结束节点
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
