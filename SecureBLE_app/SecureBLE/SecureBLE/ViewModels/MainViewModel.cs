using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Autofac;
using Plugin.BLE;
using Prism.Commands;
using Prism.Services;
using SecureBLE.Services.Interfaces;

namespace SecureBLE.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
		private readonly IPageDialogService _pageDialogService;
        public ICommand ScanDevicesCommand { get; set; }

		public MainViewModel(IPageDialogService pageDialogService)
		{
			_pageDialogService = pageDialogService;
            ScanDevicesCommand = new DelegateCommand(async () => await ScanDevices());
		}

		private async Task ScanDevices()
		{
            var ble = CrossBluetoothLE.Current;
            var notificationService = new Prism.Services.DependencyService().Get<INotificationService>();

            if (!ble.IsAvailable)
            {
                notificationService.Snackbar("Device does not support Bluetooth.");
                return;
            }

            if (!ble.IsOn)
            {
                await _pageDialogService.DisplayAlertAsync("Alert", "Please turn on Bluetooth!", "OK");

                ble.StateChanged += (s, e) =>
                {
                    notificationService.Snackbar("Bluetooth is enabled.");
                };
            }

			var bootstrapper = new Startup.Bootstrapper();
			var container = bootstrapper.Bootstrap();
			await Application.Current.MainPage.Navigation.PushAsync(container.Resolve<Views.DevicesView>());
        }
	}
}
