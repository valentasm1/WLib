using System.ComponentModel;
using Xamarin.Forms;
using Wlib.Core.Mobile.Demo.ViewModels;

namespace Wlib.Core.Mobile.Demo.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}