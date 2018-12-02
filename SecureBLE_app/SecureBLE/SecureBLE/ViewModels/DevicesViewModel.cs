using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Prism.Commands;
using Prism.Services;

namespace SecureBLE.ViewModels
{
    public class DevicesViewModel : BaseViewModel
    {
		private readonly IPageDialogService _pageDialogService;
		public ICommand SelectDeviceCommand { get; set; }

		private ObservableCollection<IDevice> _discoveredDevices;
		public ObservableCollection<IDevice> DiscoveredDevices
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

		private ObservableCollection<IService> _services;
		public ObservableCollection<IService> Services
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
			SelectDeviceCommand = new DelegateCommand(async () => await SelectDevice());

			DiscoveredDevices = new ObservableCollection<IDevice>();
			SelectedDevice = null;
			Services = new ObservableCollection<IService>();

			ScanDevices();
		}

		private async void ScanDevices()
		{
			var bleAdapter = CrossBluetoothLE.Current.Adapter;
			bleAdapter.ScanTimeout = 5000;

			DiscoveredDevices = new ObservableCollection<IDevice>();
			bleAdapter.DeviceDiscovered += (s, a) => { DiscoveredDevices.Add(a.Device);	};

			await bleAdapter.StartScanningForDevicesAsync();
		}

		private async Task SelectDevice()
		{
			Services = new ObservableCollection<IService>(await SelectedDevice.GetServicesAsync());
		}
    }
}
