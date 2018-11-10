using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Android.Support.Design.Widget;
using Java.Lang;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.Exceptions;
using Xamarin.Forms;

namespace ArduinoApp.ViewModels
{
    public class DevicesListViewModel : ViewModelBase
    {
        private readonly IAdapter _adapter = CrossBluetoothLE.Current.Adapter;

        private List<IDevice> _discoveredDevices;

        public List<IDevice> DiscoveredDevices
        {
            get => _discoveredDevices;
            set { _discoveredDevices = value; RaisePropertyChanged();}
        }

        private IDevice _selectedDevice;

        public IDevice SelectedDevice
        {
            get => _selectedDevice;
            set { _selectedDevice = value; RaisePropertyChanged(); ConnectToDevice();}
        }

        private IList<IService> _services;

        public IList<IService> Services
        {
            get { return _services; }
            set { _services = value; RaisePropertyChanged();}
        }


        public DevicesListViewModel()
        {
            Initialize();
        }

        public async void Initialize()
        {
            SelectedDevice = null;
            Services = null;

            await ScanDevices();
        }

        public async Task ScanDevices()
        {
            _adapter.ScanTimeout = 5000;
            DiscoveredDevices = new List<IDevice>();
            _adapter.DeviceDiscovered += (s, a) => { DiscoveredDevices.Add(a.Device); };
            await _adapter.StartScanningForDevicesAsync();
        }

        public async void ConnectToDevice()
        {
            //TODO: replace Context with local context
            var contentView = (Forms.Context as Activity)?.FindViewById(Android.Resource.Id.Content);

            if (SelectedDevice == null) return;

            try
            {
                await _adapter.ConnectToDeviceAsync(SelectedDevice);
                Snackbar.Make(contentView, "Succesfully connected to device!", Snackbar.LengthLong).Show();
            }
            catch (DeviceConnectionException e)
            {
                Snackbar.Make(contentView, "Could not connect to chosen device.", Snackbar.LengthLong).Show();
            }
            catch (Exception e)
            {
                Snackbar.Make(contentView, "Something went wrong!", Snackbar.LengthLong).Show();
            }

            Services = await SelectedDevice.GetServicesAsync();
        }
    }
}
