﻿using System.Collections.Generic;

namespace WLib.Core.Mobile.Services.Utils
{
    public static class DictionaryExtensions
    {
        public static string AsQueryString(this Dictionary<string, string> args)
        {
            string resultQuery = null;

            if (args != null && args.Count > 0)
            {
                List<string> argList = new List<string>();
                resultQuery = "?";
                foreach (var arg in args)
                {
                    argList.Add(arg.Key + "=" + arg.Value);
                }
                resultQuery = resultQuery + string.Join("&", argList);
            }

            return resultQuery;
        }
    }
}
