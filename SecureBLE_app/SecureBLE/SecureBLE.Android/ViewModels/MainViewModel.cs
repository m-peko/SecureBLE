using System.Threading.Tasks;
using System.Windows.Input;
using Android.App;
using Android.Bluetooth;
using Android.Support.Design.Widget;
using Prism.Commands;
using Prism.Common;
using Prism.Services;
using SecureBLE.ViewModels;
using Xamarin.Forms;

namespace SecureBLE.Droid.ViewModels
{
	class MainViewModel : BaseViewModel
	{
		private readonly IPageDialogService _pageDialogService;

		public ICommand ScanArduinoDevicesCommand { get; }

		public MainViewModel()
		{
			_pageDialogService = new PageDialogService(new ApplicationProvider());

			ScanArduinoDevicesCommand = new DelegateCommand(async () => await ScanArduinoDevices());
		}

		private async Task ScanArduinoDevices()
		{
			//TODO: replace Context with local context
			var contentView = (Forms.Context as Activity)?.FindViewById(Android.Resource.Id.Content);
			BluetoothAdapter mBluetoothAdapter = BluetoothAdapter.DefaultAdapter;

			// Device does not support Bluetooth
			if (mBluetoothAdapter == null)
			{
				Snackbar.Make(contentView, "Device doesn't support Bluetooth.", Snackbar.LengthLong).Show();
				return;
			}

			//Buletooth is not enabled
			if (!mBluetoothAdapter.IsEnabled)
			{
				//ask to enable bluetooth
				var answer = await _pageDialogService.DisplayAlertAsync("Alert", "Do you want to enable Bluetooth?", "Yes", "No");
				if (!answer) return;

				//enable bluetooth and display message
				mBluetoothAdapter.Enable();
				Snackbar.Make(contentView, "Bluetooth is enabled.", Snackbar.LengthLong).Show();
			}

			//await Application.Current.MainPage.Navigation.PushAsync(new DevicesListView());
		}
	}
}