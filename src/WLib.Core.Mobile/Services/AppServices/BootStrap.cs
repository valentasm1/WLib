using DryIoc;
using Splat.DryIoc;
using WLib.Core.Mobile.Services.Locator;

namespace WLib.Core.Mobile.Services.AppServices
{
    public class CoreBootStrap
    {
        public static Xamarin.Forms.Shell App { get; private set; }
        public static IDependencyResolver IoC { get; private set; }

        public static IDependencyRegistry DependencyRegistry { get; private set; }

        public CoreBootStrap(Xamarin.Forms.Shell app)
        {
            App = app;
            var container = new Container();
            DependencyRegistry = new SplatDependencyRegistry(container);
            IoC = new SplatDependencyResolver(container);
            container.UseDryIocDependencyResolver();
        }

    }
}
