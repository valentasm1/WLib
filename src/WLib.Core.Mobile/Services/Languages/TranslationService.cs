using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace WLib.Core.Mobile.Services.Languages
{
    public interface ITranslationService
    {
        CultureInfo CurrentCulture { get; }

        void SetCulture(CultureInfo cultureInfo);

        string Translate(string key);
    }


}