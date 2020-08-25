using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wlib.Core.Mobile.Demo.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wlib.Core.Mobile.Demo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }
    }
}