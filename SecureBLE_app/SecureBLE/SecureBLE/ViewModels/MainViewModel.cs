using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Common;
using Prism.Services;

namespace SecureBLE.ViewModels
{
    class MainViewModel : BaseViewModel
    {
		private readonly IPageDialogService _PageDialogService;
		public ICommand ScanDevicesCommand { get; set; }

		public MainViewModel()
		{
			_PageDialogService = new PageDialogService(new ApplicationProvider());
			ScanDevicesCommand = new DelegateCommand(async () => await ScanDevices());
		}

		public async Task ScanDevices()
		{
			//TODO: replace Context with local context
			var contentView = (Forms.Context as Activity)?.FindViewById(Android.Resource.Id.Content);

			if (bluetoothAdapter == null)
			{
				Snackbar.Make(contentView, "Device doesn't support Bluetooth.", Snackbar.LengthLong).Show();
				return;
			}

			if (!App.BLEAdapter.IsEnabled)
			{
				var answer = await _PageDialogService.DisplayAlertAsync("Alert", "Do you want to enable Bluetooth?", "Yes", "No");
				if (!answer)
				{
					return;
				}

				bluetoothAdapter.Enable();
				Snackbar.Make(contentView, "Bluetooth is enabled.", Snackbar.LengthLong).Show();
			}

			//await Application.Current.MainPage.Navigation.PushAsync(new DevicesListView());
		}
	}
}
