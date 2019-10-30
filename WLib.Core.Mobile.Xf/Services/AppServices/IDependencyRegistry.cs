using System;
using System.Collections.Generic;
using System.Text;
using DryIoc;
using Splat;
using WLib.Core.Mobile.ViewModels;
using Xamarin.Forms;

namespace WLib.Core.Mobile.Xf.Services.AppServices
{
    public interface IDependencyRegistry
    {
        void Register<TService, TImplementation>(string key = null) where TImplementation : TService;

        void RegisterSingleton<TType, TInterface>(string key = null, bool lazy = false);
        void RegisterInstance<TInterface>(object instance, bool lazy = false);

        void RegisterRoute<TViewModel, TPage>(string route) where TPage : ContentPage where TViewModel : IViewModel;
    }

    public class SplatDependencyRegistry : IDependencyRegistry
    {
        private readonly Container _container;

        public SplatDependencyRegistry(Container container)
        {
            _container = container;

        }
        public void Register<TService, TImplementation>(string key = null) where TImplementation : TService
        {
            _container.Register<TService, TImplementation>(serviceKey: key, reuse: new SingletonReuse());
            //Locator.CurrentMutable.Register(() => Activator.CreateInstance<TType>(), typeof(TInterface), key);
        }


        public void RegisterSingleton<TType, TInterface>(string key = null, bool lazy = false)
        {
            if (!lazy)
            {
                Locator.CurrentMutable.RegisterConstant(Activator.CreateInstance<TType>(), typeof(TInterface), key);
            }
            else
            {
                Locator.CurrentMutable.RegisterLazySingleton(() => Activator.CreateInstance<TType>(), typeof(TInterface), key);
            }
        }

        public void RegisterInstance<TInterface>(object instance, bool lazy = false)
        {
            _container.UseInstance<TInterface>((TInterface)instance);
        }

        public void RegisterRoute<TViewModel, TPage>(string route) where TViewModel : IViewModel where TPage : ContentPage
        {
            Routing.RegisterRoute(route, typeof(TPage));
            Register<IViewModel, TViewModel>(route);
        }
    }
}
