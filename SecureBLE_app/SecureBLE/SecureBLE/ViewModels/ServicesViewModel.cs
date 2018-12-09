using System.Collections.ObjectModel;
using Autofac;
using Plugin.BLE.Abstractions.Contracts;
using Xamarin.Forms;

namespace SecureBLE.ViewModels
{
    public class ServicesViewModel : BaseViewModel
    {
        private ObservableCollection<IService> _services;
        public ObservableCollection<IService> Services
        {
            get { return _services; }
            set
            {
                _services = value;
                RaisePropertyChanged();
            }
        }

        private IService _selectedService;
        public IService SelectedService
        {
            get { return _selectedService; }
            set
            {
                _selectedService = value;
                RaisePropertyChanged();
                GoToNextView();
            }
        }

        public ServicesViewModel(object selectedDevice)
        {
            Services = new ObservableCollection<IService>();
            SelectedService = null;
            IDevice device = (IDevice) selectedDevice;
            ShowServices(device);
        }

        private async void ShowServices(IDevice device)
        {
            if (device != null)
            {
                Services = new ObservableCollection<IService>(await device.GetServicesAsync());
            }
        }

        private async void GoToNextView()
        {
            if (SelectedService == null) return;
            var bootstrapper = new Startup.Bootstrapper();
            var container = bootstrapper.Bootstrap();
            await Application.Current.MainPage.Navigation.PushAsync(container.Resolve<Views.CharacteristicsView>(new NamedParameter("selectedService", SelectedService)));
        }
    }
}
