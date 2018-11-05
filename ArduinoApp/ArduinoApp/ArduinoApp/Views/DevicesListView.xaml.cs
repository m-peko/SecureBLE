using ArduinoApp.ViewModels;
using Xamarin.Forms.Xaml;

namespace ArduinoApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DevicesListView
	{
		public DevicesListView ()
		{
			InitializeComponent ();
		    BindingContext = new DevicesListViewModel();
		}
	}
}