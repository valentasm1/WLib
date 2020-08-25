using System;
using System.Collections.Generic;
using System.Text;

namespace WLib.Core.Bll.Model.Meta
{
    public interface ITranslation
    {
        string Key { get; set; }

        string Postprocessor { get; set; }

        string Parameters { get; set; }

        string Value { get; set; }
    }
}
