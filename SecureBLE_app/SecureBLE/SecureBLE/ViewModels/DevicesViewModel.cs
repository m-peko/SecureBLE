using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Prism.Services;

namespace SecureBLE.ViewModels
{
    public class DevicesViewModel : BaseViewModel
    {
		private readonly IPageDialogService _pageDialogService;

		private List<IDevice> _discoveredDevices;
		public List<IDevice> DiscoveredDevices
		{
			get { return _discoveredDevices; }
			set
			{
				_discoveredDevices = value;
				RaisePropertyChanged();
			}
		}

		private IDevice _selectedDevice;
		public IDevice SelectedDevice
		{
			get { return _selectedDevice; }
			set
			{
				_selectedDevice = value;
				RaisePropertyChanged();
			}
		}

		private List<IService> _services;
		public List<IService> Services
		{
			get { return _services; }
			set
			{
				_services = value;
				RaisePropertyChanged();
			}
		}

		public DevicesViewModel(IPageDialogService pageDialogService)
		{
			_pageDialogService = pageDialogService;

			DiscoveredDevices = null;
			SelectedDevice = null;
			Services = null;

			Start();
		}

		public async void Start()
		{
			await ScanDevices();
		}

		public async Task ScanDevices()
		{
			var bleAdapter = CrossBluetoothLE.Current.Adapter;
			bleAdapter.ScanTimeout = 5000;

			DiscoveredDevices = new List<IDevice>();
			bleAdapter.DeviceDiscovered += (s, a) => { DiscoveredDevices.Add(a.Device); };

			await bleAdapter.StartScanningForDevicesAsync();
		}
    }
}
