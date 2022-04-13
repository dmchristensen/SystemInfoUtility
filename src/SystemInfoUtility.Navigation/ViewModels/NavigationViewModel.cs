using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;

namespace SystemInfoUtility.Navigation.ViewModels
{
    public class NavigationViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;

        private ObservableCollection<string> _navigationItems = default!;

        public ObservableCollection<string> NavigationItems
        {
            get { return _navigationItems; }
            set { SetProperty(ref _navigationItems, value); }
        }

        private string _selectedNavigationItem = default!;

        public string SelectedNavigationItem
        {
            get { return _selectedNavigationItem; }
            set { SetProperty(ref _selectedNavigationItem, value); }
        }

        public DelegateCommand NavigateCommand { get; set; }

        public NavigationViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            NavigationItems = new ObservableCollection<string> { "System", "Network" };
            SelectedNavigationItem = NavigationItems[0];

            NavigateCommand = new DelegateCommand(OnNavigate);
        }

        private void OnNavigate()
        {
            _regionManager.RequestNavigate("ContentRegion", SelectedNavigationItem);
        }
    }
}
