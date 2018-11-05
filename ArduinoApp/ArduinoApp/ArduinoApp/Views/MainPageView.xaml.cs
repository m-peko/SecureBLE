using ArduinoApp.ViewModels;

namespace ArduinoApp.Views
{
    public partial class MainPageView
    {
        public MainPageView()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }
    }
}
