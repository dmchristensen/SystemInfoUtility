using DeviceId;
using Prism.Mvvm;
using System;
using System.Windows.Threading;

namespace SystemInfoUtility.Content.ViewModels
{
    public class SystemViewModel : BindableBase
    {
        private readonly DispatcherTimer _refreshTimer;

        private string _hostName;
        public string HostName
        {
            get { return _hostName; }
            set { SetProperty(ref _hostName, value); }
        }

        private string _currentDirectory;

        public string CurrentDirectory
        {
            get { return _currentDirectory; }
            set { SetProperty(ref _currentDirectory, value); }
        }

        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        private string _userDomainName;

        public string UserDomainName
        {
            get { return _userDomainName; }
            set { SetProperty(ref _userDomainName, value); }
        }

        private string _osVersion;

        public string OSVersion
        {
            get { return _osVersion; }
            set { SetProperty(ref _osVersion, value); }
        }

        private string _deviceID;

        public string DeviceID
        {
            get { return _deviceID; }
            set { SetProperty(ref _deviceID, value); }
        }

        private string _systemDirectory;

        public string SystemDirectory
        {
            get { return _systemDirectory; }
            set { SetProperty(ref _systemDirectory, value); }
        }

        private string _clrVersion;

        public string CLRVersion
        {
            get { return _clrVersion; }
            set { SetProperty(ref _clrVersion, value); }
        }

        private int _processID;

        public int ProcessID
        {
            get { return _processID; }
            set { SetProperty(ref _processID, value); }
        }

        private int _currentManagedThreadID;

        public int CurrentManagedThreadID
        {
            get { return _currentManagedThreadID; }
            set { SetProperty(ref _currentManagedThreadID, value); }
        }

        private int _processorCount;

        public int ProcessorCount
        {
            get { return _processorCount; }
            set { SetProperty(ref _processorCount, value); }
        }

        private string _systemUpTime;

        public string SystemUpTime
        {
            get { return _systemUpTime; }
            set { SetProperty(ref _systemUpTime, value); }
        }

        private long _workingSet;

        public long WorkingSet
        {
            get { return _workingSet; }
            set { SetProperty(ref _workingSet, value); }
        }


        public SystemViewModel()
        {
            HostName = Environment.MachineName;
            CurrentDirectory = Environment.CurrentDirectory;
            UserName = Environment.UserName;
            UserDomainName = Environment.UserDomainName;
            OSVersion = Environment.OSVersion.VersionString;
            DeviceID = new DeviceIdBuilder()
                .AddMachineName()
                .AddOsVersion()
                .AddMacAddress()
                .OnWindows(windows => windows
                    .AddProcessorId()
                    .AddMotherboardSerialNumber()
                    .AddSystemDriveSerialNumber())
                .ToString();
            SystemDirectory = Environment.SystemDirectory;
            CLRVersion = Environment.Version.ToString();
            ProcessID = Environment.ProcessId;
            CurrentManagedThreadID = Environment.CurrentManagedThreadId;
            ProcessorCount = Environment.ProcessorCount;
            SystemUpTime = GetSystemUpTime();
            WorkingSet = GetWorkingSetInMB();

            _refreshTimer = new DispatcherTimer();
            _refreshTimer.Interval = TimeSpan.FromMilliseconds(100);
            _refreshTimer.Tick += _refreshTimer_Tick;
            _refreshTimer.Start();
        }

        private void _refreshTimer_Tick(object? sender, EventArgs e)
        {
            SystemUpTime = GetSystemUpTime();
            WorkingSet = GetWorkingSetInMB();
        }

        private string GetSystemUpTime()
        {
            return TimeSpan.FromMilliseconds(Environment.TickCount).ToString(@"d\.hh\:mm\:ss");
        }

        private long GetWorkingSetInMB()
        {
            return Environment.WorkingSet / 1000000;
        }
    }
}
