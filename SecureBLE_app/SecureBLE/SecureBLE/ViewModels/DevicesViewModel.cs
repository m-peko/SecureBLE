using System.Collections.ObjectModel;
using Autofac;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.Exceptions;
using Xamarin.Forms;

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
				ConnectToSelectedDevice();
			}
		}


        public DevicesViewModel()
		{
			DiscoveredDevices = new ObservableCollection<IDevice>();
			SelectedDevice = null;

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

		private async void ConnectToSelectedDevice()
		{
			if (SelectedDevice != null)
			{
				try
				{
					var bleAdapter = CrossBluetoothLE.Current.Adapter;
					await bleAdapter.ConnectToDeviceAsync(SelectedDevice);
                    GoToNextView();
                }
				catch (DeviceConnectionException e)
				{
					System.Diagnostics.Debug.WriteLine($"Could not connect to { e.DeviceName }.");
				}
			}
		}

        private async void GoToNextView()
        {
            var bootstrapper = new Startup.Bootstrapper();
            var container = bootstrapper.Bootstrap();
            await Application.Current.MainPage.Navigation.PushAsync(container.Resolve<Views.ServicesView>(new NamedParameter("selectedDevice", SelectedDevice)));
        }
    }
}
