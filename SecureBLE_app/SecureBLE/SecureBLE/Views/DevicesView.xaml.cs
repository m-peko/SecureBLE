using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SecureBLE.ViewModels;

namespace SecureBLE.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DevicesView : ContentPage
	{
		public DevicesView(DevicesViewModel devicesViewModel)
		{
			InitializeComponent();
			BindingContext = devicesViewModel;
		}
	}
}