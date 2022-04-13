using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using SystemInfoUtility.Content.Views;
using SystemInfoUtility.Navigation.Views;

namespace SystemInfoUtility.Navigation
{
    public class NavigationModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public NavigationModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion("NavigationRegion", typeof(NavigationView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<SystemView>("System");
            containerRegistry.RegisterForNavigation<NetworkView>("Network");
        }
    }
}
