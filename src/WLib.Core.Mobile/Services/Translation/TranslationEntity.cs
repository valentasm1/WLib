using System;
using System.Collections.Generic;
using System.Text;
using Realms;
using WLib.Core.Bll.Model.Meta;
using WLib.Core.Mobile.Bll.Model;

namespace WLib.Core.Mobile.Services.Translation
{
    public class TranslationEntity : RealmObject, IMobileBusinessItem, ISyncableItem, ITranslation
    {
        [PrimaryKey]
        public string MobileId { get; set; }
        public int RemoteId { get; set; }

        public string Key { get; set; }
        public string Parameters { get; set; }
        public string Value { get; set; }

        /// <summary>
        /// en-UK
        /// https://github.com/libyal/libfwnt/wiki/Language-Code-identifiers
        /// http://www.codedigest.com/CodeDigest/207-Get-All-Language-Country-Code-List-for-all-Culture-in-C---ASP-Net.aspx
        /// </summary>
        public string Language { get; set; }
        public string Postprocessor { get; set; }
    }
}
