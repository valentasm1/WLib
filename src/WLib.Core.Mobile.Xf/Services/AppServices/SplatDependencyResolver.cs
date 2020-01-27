
using System.Diagnostics;
using DryIoc;
using Splat;
using Splat.DryIoc;
using IDependencyResolver = WLib.Core.Mobile.Services.Locator.IDependencyResolver;

namespace WLib.Core.Mobile.Xf.Services.AppServices
{
    public class SplatDependencyResolver : DryIocDependencyResolver, IDependencyResolver
    {
        private readonly Container _container;

        public SplatDependencyResolver(Container container) : base(container)
        {
            _container = container;
        }
        public T Resolve<T>(string key = null)
        {
            var instance = Locator.Current.GetService<T>(key);

            //var model = _container.Resolve<T>(serviceKey: key, ifUnresolved: IfUnresolved.Throw);
            if (instance == null)
            {
                Debug.WriteLine($"Could not resolve {typeof(T).Name} for {key}");
            }

            return instance;
        }
    }
}
