using System;
using DryIoc;
using Splat;
using WLib.Core.Mobile.ViewModels;
using Xamarin.Forms;

namespace WLib.Core.Mobile.Services.AppServices
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

        //https://bitbucket.org/dadhi/dryioc/wiki/ReuseAndScopes
        //TODO fix
        public void Register<TService, TImplementation>(string key = null) where TImplementation : TService
        {
            _container.Register<TService, TImplementation>(serviceKey: key, reuse: new SingletonReuse());
            //Locator.CurrentMutable.Register(() => Activator.CreateInstance<TType>(), typeof(TInterface), key);
        }


        public void RegisterSingleton<TType, TInterface>(string key = null, bool lazy = false)
        {
            if (!lazy)
            {
                Splat.Locator.CurrentMutable.RegisterConstant(Activator.CreateInstance<TType>(), typeof(TInterface), key);
            }
            else
            {
                Splat.Locator.CurrentMutable.RegisterLazySingleton(() => Activator.CreateInstance<TType>(), typeof(TInterface), key);
            }
        }

        public void RegisterInstance<TInterface>(object instance, bool lazy = false)
        {
            _container.UseInstance<TInterface>((TInterface)instance);
        }

        /// <summary>
        /// Using shell Routing.RegisterRoute(route, typeof(TPage));
        /// AND
        /// Register<IViewModel, TViewModel>(route);
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <typeparam name="TPage"></typeparam>
        /// <param name="route"></param>
        public void RegisterRoute<TViewModel, TPage>(string route) where TViewModel : IViewModel where TPage : ContentPage
        {
            Routing.RegisterRoute(route, typeof(TPage));
            Register<IViewModel, TViewModel>(route);
        }
    }
}
