using System;
using System.Collections.Generic;
using System.Text;
using WLib.Core.Bll.Model.Meta;

namespace WLib.Core.Mobile.Services
{
    public interface ITranslationPostprocessor
    {
        string Process(IEnumerable<ITranslation> input);
    }
}
