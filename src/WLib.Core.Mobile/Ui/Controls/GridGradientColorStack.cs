using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace WLib.Core.Mobile.Ui.Controls
{
    public class GridGradient : Grid
    {
        public static readonly BindableProperty StartColorProperty = BindableProperty.Create(nameof(StartColor), typeof(Color), typeof(GridGradient), default(Color));
        public Xamarin.Forms.Color StartColor
        {
            get => (Xamarin.Forms.Color)GetValue(StartColorProperty);
            set => SetValue(StartColorProperty, value);
        }

        public static readonly BindableProperty EndColorProperty = BindableProperty.Create(nameof(EndColor), typeof(Color), typeof(GridGradient), default(Color));
        public Xamarin.Forms.Color EndColor
        {
            get => (Xamarin.Forms.Color)GetValue(EndColorProperty);
            set => SetValue(EndColorProperty, value);
        }
    }
}
