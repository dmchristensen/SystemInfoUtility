using Prism.Mvvm;
using System.Reflection;

namespace SystemInfoUtility.ViewModels
{
    public class ShellWindowViewModel : BindableBase
    {
        private string _applicationVerion = default!;

        public string ApplicationVersion
        {
            get { return _applicationVerion; }
            set { SetProperty(ref _applicationVerion, value); }
        }

        public ShellWindowViewModel()
        {
            var assembly = Assembly.GetEntryAssembly();
            ApplicationVersion = $"v{assembly.GetName().Version.ToString()}";
        }
    }
}
