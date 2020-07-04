using System;
using System.Collections.Generic;
using System.Text;

namespace QiQiTemplate
{
    public class IFModel
    {
        public string LogOper { get; set; }// &(且)|(或)
        public FieldType LeftType { get; set; }
        public string Left { get; set; }
        public FieldType RightType { get; set; }
        public string Right { get; set; }
        public string Oper { get; set; }
    }
}
