using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SecureBLE
{
	public partial class App : Application
	{
		public static IAdapter BLEAdapter { get; set; }

		public App()
		{
			InitializeComponent();

			MainPage = new MainPage();

			BLEAdapter = CrossBluetoothLE.Current.Adapter;
		}

		protected override void OnStart()
		{}

		protected override void OnSleep()
		{}

		protected override void OnResume()
		{}
	}
}
