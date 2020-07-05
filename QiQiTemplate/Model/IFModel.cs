using System;
using System.Collections.Generic;
using System.Text;

namespace QiQiTemplate
{
    public class IFModel
    {
        private string _logOper;
        public string LogOper
        {
            get { return this._logOper; }
            set
            {
                if (value != "&" || value != "|")
                {
                    this._logOper = "&";
                }
                else
                {
                    this._logOper = value;
                }
            }
        }
        public FieldType LeftType { get; set; }
        public string Left { get; set; }
        public FieldType RightType { get; set; }
        public string Right { get; set; }
        public string Oper { get; set; }
    }
}
