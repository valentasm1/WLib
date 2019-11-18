using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace WLib.Core.Mobile.Xf.Controls
{
    public partial class ListLinkButton : ContentView
    {

        public static readonly BindableProperty IconCodeProperty = BindableProperty.Create("IconCode", typeof(string), typeof(string), string.Empty);

        public string IconCode
        {
            get => (string)GetValue(IconCodeProperty);
            set => SetValue(IconCodeProperty, value);
        }

        public static readonly BindableProperty IconFontFamilyProperty = BindableProperty.Create(nameof(IconFontFamily), typeof(string), typeof(string), string.Empty);

        public string IconFontFamily
        {
            get => (string)GetValue(IconFontFamilyProperty);
            set => SetValue(IconFontFamilyProperty, value);
        }

        public static readonly BindableProperty TextFontFamilyProperty = BindableProperty.Create(nameof(TextFontFamily), typeof(string), typeof(string), string.Empty);

        public string TextFontFamily
        {
            get => (string)GetValue(TextFontFamilyProperty);
            set => SetValue(TextFontFamilyProperty, value);
        }


        public static readonly BindableProperty TextProperty = BindableProperty.Create("Text", typeof(string), typeof(string), string.Empty);

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ListLinkButton), default(ICommand));

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(ListLinkButton));
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }


        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public ListLinkButton()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            if (Command != null && Command.CanExecute(CommandParameter))
            {
                //await MainFrame.ScaleTo(0.9, 50, Easing.BounceIn);
                //await MainFrame.ScaleTo(1, 50, Easing.BounceIn);

                Command?.Execute(CommandParameter);
            }
        }


    }
}