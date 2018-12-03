using System.Collections.ObjectModel;
using System.Text;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;

namespace SecureBLE.ViewModels
{
    public class DevicesViewModel : BaseViewModel
    {
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
			    ShowServices();

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

        private IService _selectedService;

        public IService SelectedService
        {
            get { return _selectedService; }
            set
            {
                _selectedService = value;
                RaisePropertyChanged();
                ShowCharacteristics();
            }
        }

        private ObservableCollection<ICharacteristic> _characteristics;

        public ObservableCollection<ICharacteristic> Characteristics
        {
            get { return _characteristics; }
            set
            {
                _characteristics = value;
                RaisePropertyChanged();
            }
        }

        private ICharacteristic _selectedCharacteristic;

        public ICharacteristic SelectedCharacteristic
        {
            get { return _selectedCharacteristic; }
            set
            {
                _selectedCharacteristic = value;
                RaisePropertyChanged();
                Write();
            }
        }


        public DevicesViewModel()
		{
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

		private async void ShowServices()
		{
            if(SelectedDevice == null) return;

			Services = new ObservableCollection<IService>(await SelectedDevice.GetServicesAsync());
		}

        private async void ShowCharacteristics()
        {
            if (SelectedService == null) return;

            Characteristics = new ObservableCollection<ICharacteristic>(await SelectedService.GetCharacteristicsAsync());
        }

        private void Write()
        {
            //TODO: use communication service

            if(SelectedCharacteristic == null) return;

            var message = "$CONNECT;";
            byte[] bytes = Encoding.ASCII.GetBytes(message);

            if (SelectedCharacteristic.CanWrite)
            {
                SelectedCharacteristic.WriteAsync(bytes);
            }
        }
    }
}
