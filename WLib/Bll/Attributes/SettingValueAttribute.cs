using System;
using System.Collections.Generic;
using System.Text;

namespace WLib.Core.Bll.Attributes
{
    public class SettingValueAttribute : Attribute
    {
        public string UniqueCode { get; set; }
    }
}
