using Autofac;
using SecureBLE.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SecureBLE.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CommunicationView : ContentPage
    {
        public CommunicationView(object selectedCharacteristic)
        {
            InitializeComponent();
            var bootstrapper = new Startup.Bootstrapper();
            var container = bootstrapper.Bootstrap();

            BindingContext = container.Resolve<CharacteristicsViewModel>(new NamedParameter("selectedCharacteristic", selectedCharacteristic));
        }
    }
}