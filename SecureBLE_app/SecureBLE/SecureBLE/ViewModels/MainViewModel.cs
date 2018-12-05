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

			ble.StateChanged += (s, e) =>
			{
				if (ble.IsOn)
				{
					notificationService.Snackbar("Bluetooth is enabled.");
					GoToNextView();
				}
			};

			if (!ble.IsOn)
			{
				await _pageDialogService.DisplayAlertAsync("Alert", "Please turn on Bluetooth!", "OK");
			}
			else
			{
				GoToNextView();
			}
        }

        private async void GoToNextView()
        {
            var bootstrapper = new Startup.Bootstrapper();
            var container = bootstrapper.Bootstrap();
            await Application.Current.MainPage.Navigation.PushAsync(container.Resolve<Views.DevicesView>());
        }
	}
}
