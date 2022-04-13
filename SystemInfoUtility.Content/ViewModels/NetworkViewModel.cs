using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace SystemInfoUtility.Content.ViewModels
{
    public class NetworkViewModel : BindableBase
    {
        private ObservableCollection<string> _availableIpv4Addresses;

        public ObservableCollection<string> AvailableIPv4Addresses
        {
            get { return _availableIpv4Addresses; }
            set { SetProperty(ref _availableIpv4Addresses, value); }
        }

        private string _selectedIpv4Address;

        public string SelectedIPv4Address
        {
            get { return _selectedIpv4Address; }
            set { SetProperty(ref _selectedIpv4Address, value); }
        }

        public DelegateCommand IpSelectionChangedCommand { get; set; }

        private string _subnetMask;

        public string SubnetMask
        {
            get { return _subnetMask; }
            set { _subnetMask = value; }
        }


        public NetworkViewModel()
        {
            AvailableIPv4Addresses = new ObservableCollection<string>();
            AvailableIPv4Addresses.AddRange(GetLocalIPAddresses());
            if (AvailableIPv4Addresses.Any())
            {
                SelectedIPv4Address = AvailableIPv4Addresses[0];
                SubnetMask = GetSubnetMask(SelectedIPv4Address);
            }
            IpSelectionChangedCommand = new DelegateCommand(OnIpSelectionChanged);
        }

        private void OnIpSelectionChanged()
        {
            SubnetMask = GetSubnetMask(SelectedIPv4Address);
        }

        private List<string> GetLocalIPAddresses()
        {
            List<string> result = new List<string>();

            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        result.Add(ip.ToString());
                    }
                }
            }

            return result;
        }

        private string GetSubnetMask(string ipv4Address)
        {
            uint firstOctet = GetFirtsOctet(ipv4Address);
            if (firstOctet >= 0 && firstOctet <= 127)
                return "255.0.0.0";
            else if (firstOctet >= 128 && firstOctet <= 191)
                return "255.255.0.0";
            else if (firstOctet >= 192 && firstOctet <= 223)
                return "255.255.255.0";
            else return "0.0.0.0";
        }

        private uint GetFirtsOctet(string ipAddress)
        {
            IPAddress iPAddress = IPAddress.Parse(ipAddress);
            byte[] byteIP = iPAddress.GetAddressBytes();
            uint ipInUint = byteIP[0];
            return ipInUint;
        }

    }
}
