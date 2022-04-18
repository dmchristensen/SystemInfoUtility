using Prism.Mvvm;
using System.Reflection;
using AutoUpdaterDotNET;
using System.Windows.Threading;
using System;

namespace SystemInfoUtility.ViewModels
{
    public class ShellWindowViewModel : BindableBase
    {
        private readonly DispatcherTimer _updateTimer;
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

            AutoUpdater.LetUserSelectRemindLater = true;
            AutoUpdater.RemindLaterTimeSpan = RemindLaterFormat.Minutes;
            AutoUpdater.RemindLaterAt = 1;
            AutoUpdater.ReportErrors = true;

            _updateTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(30) };

            _updateTimer.Tick += _updateTimer_Tick;
            _updateTimer.Start();
        }

        private void _updateTimer_Tick(object? sender, EventArgs e)
        {
            AutoUpdater.Start(@"https://qa.stratumglobal.com/update-wpf/version.xml");
        }
    }
}
