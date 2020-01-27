using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WLib.Core.Mobile.Xf.Services.Utils
{
    public interface IAppUtils
    {
        bool IsConnected();

        Task<string> GetDeviceId();
    }



    public class AppUtils : IAppUtils
    {
        //https://docs.microsoft.com/en-us/xamarin/essentials/maps?context=xamarin%2Fxamarin-forms&tabs=android

        public bool IsConnected()
        {
            var current = Connectivity.NetworkAccess;

            switch (current)
            {
                case NetworkAccess.None:
                case NetworkAccess.Unknown:
                    return false;
                case NetworkAccess.Local:
                case NetworkAccess.ConstrainedInternet:
                case NetworkAccess.Internet:
                    return true;
                default:
                    return true;
            }
        }

        public virtual async Task<string> GetDeviceId()
        {
            var userName = $"{DeviceInfo.Name} {DeviceInfo.Model} {DeviceInfo.Platform}";

            return userName;
        }
        public static Dictionary<string, string> ParseNullableQuery(string queryString)
        {
            var accumulator = new Dictionary<string, string>();

            if (string.IsNullOrEmpty(queryString) || queryString == "?")
            {
                return null;
            }

            int scanIndex = 0;
            if (queryString[0] == '?')
            {
                scanIndex = 1;
            }

            int textLength = queryString.Length;
            int equalIndex = queryString.IndexOf('=');
            if (equalIndex == -1)
            {
                equalIndex = textLength;
            }
            while (scanIndex < textLength)
            {
                int delimiterIndex = queryString.IndexOf('&', scanIndex);
                if (delimiterIndex == -1)
                {
                    delimiterIndex = textLength;
                }
                if (equalIndex < delimiterIndex)
                {
                    while (scanIndex != equalIndex && char.IsWhiteSpace(queryString[scanIndex]))
                    {
                        ++scanIndex;
                    }
                    string name = queryString.Substring(scanIndex, equalIndex - scanIndex);
                    string value = queryString.Substring(equalIndex + 1, delimiterIndex - equalIndex - 1);
                    accumulator.Add(
                        Uri.UnescapeDataString(name.Replace('+', ' ')),
                        Uri.UnescapeDataString(value.Replace('+', ' ')));
                    equalIndex = queryString.IndexOf('=', delimiterIndex);
                    if (equalIndex == -1)
                    {
                        equalIndex = textLength;
                    }
                }
                else
                {
                    if (delimiterIndex > scanIndex)
                    {
                        accumulator.Add(queryString.Substring(scanIndex, delimiterIndex - scanIndex), string.Empty);
                    }
                }
                scanIndex = delimiterIndex + 1;
            }



            return accumulator;
        }

        public static Color HexToColor(string hexColor, double opacity)
        {
            //Remove # if present
            if (hexColor.IndexOf('#') != -1)
                hexColor = hexColor.Replace("#", "");

            int red = 0;
            int green = 0;
            int blue = 0;

            if (hexColor.Length == 6)
            {
                //#RRGGBB
                red = int.Parse(hexColor.Substring(0, 2), NumberStyles.AllowHexSpecifier);
                green = int.Parse(hexColor.Substring(2, 2), NumberStyles.AllowHexSpecifier);
                blue = int.Parse(hexColor.Substring(4, 2), NumberStyles.AllowHexSpecifier);
            }
            else if (hexColor.Length == 3)
            {
                //#RGB
                red = int.Parse(hexColor[0].ToString() + hexColor[0].ToString(), NumberStyles.AllowHexSpecifier);
                green = int.Parse(hexColor[1].ToString() + hexColor[1].ToString(), NumberStyles.AllowHexSpecifier);
                blue = int.Parse(hexColor[2].ToString() + hexColor[2].ToString(), NumberStyles.AllowHexSpecifier);
            }
            else if (hexColor.Length == 8)
            {
                var col = Color.FromRgba(
                    int.Parse(hexColor.Substring(0, 2), NumberStyles.HexNumber),
                    int.Parse(hexColor.Substring(2, 2), NumberStyles.HexNumber),
                    int.Parse(hexColor.Substring(4, 2), NumberStyles.HexNumber),
                    int.Parse(hexColor.Substring(6, 2), NumberStyles.HexNumber));

                //https://stackoverflow.com/questions/2109756/how-do-i-get-the-color-from-a-hexadecimal-color-code-using-net/2109771

                return col;

            }

            var result = Color.FromRgba(red, green, blue, opacity);
            return result;
        }
    }
}
