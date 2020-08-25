using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text;

namespace WLib.Core.Services
{
    public static class CoreUtils
    {
        private static Stack<Color> _lightColorStack;

        /// <summary>
        /// Contains ~20 color. When all used it itterate them
        /// </summary>
        public static Color RandLightColor => GetColor();

        private static Color GetColor()
        {
            if (_lightColorStack == null)
            {
                InitColors();
            }

            if (_lightColorStack.Count == 0)
            {
                InitColors();
            }

            var color = _lightColorStack.Pop();

            return color;
        }


        private static void InitColors()
        {
            _lightColorStack = new Stack<Color>(20);
            _lightColorStack.Push(FromHtml("#e6194B"));
            _lightColorStack.Push(FromHtml("#3cb44b"));
            _lightColorStack.Push(FromHtml("#ffe119"));
            _lightColorStack.Push(FromHtml("#4363d8"));
            _lightColorStack.Push(FromHtml("#f58231"));
            _lightColorStack.Push(FromHtml("#911eb4"));
            _lightColorStack.Push(FromHtml("#42d4f4"));
            _lightColorStack.Push(FromHtml("#f032e6"));
            _lightColorStack.Push(FromHtml("#bfef45"));
            _lightColorStack.Push(FromHtml("#fabebe"));
            _lightColorStack.Push(FromHtml("#469990"));
            _lightColorStack.Push(FromHtml("#e6beff"));
            _lightColorStack.Push(FromHtml("#9A6324"));
            _lightColorStack.Push(FromHtml("#fffac8"));
            _lightColorStack.Push(FromHtml("#800000"));
            _lightColorStack.Push(FromHtml("#aaffc3"));
            _lightColorStack.Push(FromHtml("#808000"));
            _lightColorStack.Push(FromHtml("#ffd8b1"));
            _lightColorStack.Push(FromHtml("#7FC9FF"));
            _lightColorStack.Push(FromHtml("#a9a9a9"));
        }

        private static Color FromHtml(string colorcode)
        {
            colorcode = colorcode.TrimStart('#');

            Color col; // from System.Drawing or System.Windows.Media
            if (colorcode.Length == 6)
                col = Color.FromArgb(255, // hardcoded opaque
                    int.Parse(colorcode.Substring(0, 2), NumberStyles.HexNumber),
                    int.Parse(colorcode.Substring(2, 2), NumberStyles.HexNumber),
                    int.Parse(colorcode.Substring(4, 2), NumberStyles.HexNumber));
            else // assuming length of 8
                col = Color.FromArgb(
                    int.Parse(colorcode.Substring(0, 2), NumberStyles.HexNumber),
                    int.Parse(colorcode.Substring(2, 2), NumberStyles.HexNumber),
                    int.Parse(colorcode.Substring(4, 2), NumberStyles.HexNumber),
                    int.Parse(colorcode.Substring(6, 2), NumberStyles.HexNumber));

            return col;
        }
    }
}