using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using SystemInfoUtility.Content.Views;

namespace SystemInfoUtility.Content
{
    public class ContentModule : IModule
    {
        private readonly IRegionManager _regionManager;
        public ContentModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion("ContentRegion", typeof(SystemView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}
