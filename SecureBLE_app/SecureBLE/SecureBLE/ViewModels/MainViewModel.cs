using System.Threading.Tasks;
using System.Windows.Input;
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

		public async Task ScanDevices()
		{
            var ble = CrossBluetoothLE.Current;
            var notificationService = new DependencyService().Get<INotificationService>();

            if (!ble.IsAvailable)
            {
                notificationService.Snackbar("Device doesn't support Bluetooth.");
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

			var bootstrapper = new SecureBLE.Startup.Bootstrapper();
			var container = bootstrapper.Bootstrap();
			await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(container.Resolve<SecureBLE.Views.DevicesView>());
        }
	}
}
