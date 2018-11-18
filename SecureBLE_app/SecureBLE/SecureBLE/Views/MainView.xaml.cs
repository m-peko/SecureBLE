using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SecureBLE.ViewModels;

namespace SecureBLE.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainView : ContentPage
	{
		public MainView(MainViewModel mainViewModel)
		{
			InitializeComponent();
			BindingContext = mainViewModel;
		}
	}
}