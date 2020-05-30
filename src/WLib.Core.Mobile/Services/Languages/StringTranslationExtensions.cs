using System;
using System.Collections.Generic;
using System.Text;
using WLib.Core.Mobile.Services.AppServices;

namespace WLib.Core.Mobile.Services.Languages
{
    public static class StringTranslationExtensions
    {
        private static readonly ITranslationService _translationService;
        static StringTranslationExtensions()
        {
            _translationService = CoreBootStrap.IoC.Resolve<ITranslationService>();
        }
        /// <summary>
        /// Translate the text automatically
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Translate(this string text)
        {
            if (text != null)
            {
                return _translationService.Translate(text);
                //var assembly = typeof(StringExtensions).GetTypeInfo().Assembly;
                //var assemblyName = assembly.GetName();
                //ResourceManager resourceManager = new ResourceManager($"{assemblyName.Name}.Resources", assembly);
                //var lg = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
                //return resourceManager.GetString(text, new CultureInfo(lg));
            }

            return text;
        }
    }
}
