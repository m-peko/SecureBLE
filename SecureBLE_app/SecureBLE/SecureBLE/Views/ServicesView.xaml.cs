using Autofac;
using SecureBLE.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SecureBLE.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ServicesView : ContentPage
	{
		public ServicesView (object selectedDevice)
		{
			InitializeComponent ();

		    var bootstrapper = new Startup.Bootstrapper();
		    var container = bootstrapper.Bootstrap();

            BindingContext = container.Resolve<ServicesViewModel>(new NamedParameter("selectedDevice", selectedDevice));
        }
	}
}