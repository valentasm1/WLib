using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using WLib.Core.Bll.Model.Meta;
using WLib.Core.Mobile.Services.Data;
using WLib.Core.Mobile.Services.Languages;
using WLib.Core.Mobile.Services.Translation;

namespace WLib.Core.Mobile.Services
{
    public class TranslationService : ITranslationService
    {
        private readonly IRepository _repository;
        private Dictionary<string, List<ITranslation>> _cache = new Dictionary<string, List<ITranslation>>();

        public TranslationService(IRepository repository)
        {
            _repository = repository;
        }

        public CultureInfo CurrentCulture { get; private set; }

        public void SetCulture(CultureInfo cultureInfo)
        {
            CurrentCulture = cultureInfo;
            _cache.Clear();
        }

        public string Translate(string key)
        {
            if (_cache.TryGetValue(key, out var resultList))
            {
                return resultList?.FirstOrDefault()?.Value ?? key;
            }

            resultList = new List<ITranslation>();
            var alll = _repository.Instance.All<TranslationEntity>().ToList();

            var existingTranslations = _repository.Instance.All<TranslationEntity>().Where(x => x.Key == key && x.Language == CurrentCulture.Name).ToList();
            if (!existingTranslations.Any()) return key;

            resultList.AddRange(existingTranslations);
            var firstInfo = existingTranslations.First();
            _cache.Add(key, resultList);

            if (!string.IsNullOrEmpty(firstInfo.Postprocessor))
            {
                var processor = Activator.CreateInstance(Type.GetType(firstInfo.Postprocessor)) as ITranslationPostprocessor;
                if (processor == null) return key;

                return processor.Process(existingTranslations);
            }


            return firstInfo.Value;
        }
    }
}
