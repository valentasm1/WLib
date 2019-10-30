using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using WLib.Core.Mobile.Services.Navigation;
using WLib.Core.Mobile.Xf.Services.Utils;
using Xamarin.Forms;

namespace WLib.Core.Mobile.Xf.Services.AppServices
{
    public class NavigationService : INavigationService
    {
        private bool _initialized;

        private NavigableElement _navigationRoot;

        private Xamarin.Forms.Shell _shell => CoreBootStrap.App as Xamarin.Forms.Shell;

        private NavigableElement NavigationRoot
        {
            get => GetShellSection(_navigationRoot) ?? _navigationRoot;
            set => _navigationRoot = value;
        }

        private void Shell_Navigated(object sender, ShellNavigatedEventArgs e)
        {
            Debug.WriteLine($"Navigated to {e.Current.Location} from {e.Previous?.Location} navigation type{e.Source.ToString()}");
        }

        private void Shell_Navigating(object sender, ShellNavigatingEventArgs e)
        {
            //TODO: Hook e.Cancel into viewmodel			
        }

        private async Task NavigateShellAsync(string navigationRoute, Dictionary<string, string> args, bool animated = true)
        {
            var queryString = args.AsQueryString();
            navigationRoute = navigationRoute + queryString;
            Debug.WriteLine($"Shell Navigating to {navigationRoute}");
            await _shell.GoToAsync(navigationRoute, true);
        }

        public async Task GoBackAsync(bool fromModal = false)
        {
            if (!fromModal)
            {
                await Shell.Current.Navigation.PopAsync();
                //await NavigationRoot.Navigation.PopAsync();
            }
            else
            {
                await NavigationRoot.Navigation.PopModalAsync();
            }
        }

        public async Task NavigateToAsync(string navigationRoute, Dictionary<string, string> args = null, NavigationOptions options = null)
        {

            //IView view = App.IoC.Resolve<IView>(navigationRoute);
            //var page = view as Page;
            //if (page == null)
            //{
            //    Debug.WriteLine($"Could not resolve view for {navigationRoute}: Assuming this is a shell route...");
            //    await NavigateShellAsync(navigationRoute, args, options.Animated);
            //    return;
            //}

            //if (page is MvvmContentPage mvvmPage)
            //{
            //    mvvmPage.NavigationArgs = args;
            //}

            options = options ?? NavigationOptions.Default();

            if (options.CloseFlyout)
            {
                _shell.FlyoutIsPresented = false;
                await Task.Delay(TimeSpan.FromSeconds(0.5f));
                //await _shell.FlyoutIsPresented = false;
            }

            var argsIndex = navigationRoute.IndexOf("?");
            if (argsIndex != -1)
            {
                args = AppUtils.ParseNullableQuery(navigationRoute);
            }

            //if (args != null)
            //{
            //    //await Shell.Current.GoToAsync($"//animals/elephants/elephantdetails?name={elephantName}");
            //    //StringBuilder routeParams = new StringBuilder();
            //    //foreach (var arg in args)
            //    //{
            //    //    routeParams.Append(arg.Key).Append("=").Append(arg.Value);
            //    //}

            //    navigationRoute += "?" + routeParams.ToString();
            //}

            await Shell.Current.GoToAsync(navigationRoute);

            //if (options.Modal)
            //{


            //    //await NavigationRoot.Navigation.PushModalAsync(page, options.Animated).ConfigureAwait(false);
            //}
            //else
            //{
            //    await NavigationRoot.Navigation.PushAsync(page, options.Animated).ConfigureAwait(false);
            //}
        }

        public void Initialize(NavigableElement navigationRootPage)
        {
            if (_initialized)
            {
                return;
            }

            _initialized = true;
            NavigationRoot = navigationRootPage;
            _shell.Navigating += Shell_Navigating;
            _shell.Navigated += Shell_Navigated;
        }

        // Provides a navigatable section for elements which aren't explicitly defined within the Shell. For example,
        // if it's accessed from the fly-out through a MenuItem but it doesn't belong to any section
        internal static ShellSection GetShellSection(Element element)
        {
            if (element == null)
            {
                return null;
            }

            var parent = element;
            var parentSection = parent as ShellSection;

            while (parentSection == null && parent != null)
            {
                parent = parent.Parent;
                parentSection = parent as ShellSection;
            }

            return parentSection;
        }
    }
}
