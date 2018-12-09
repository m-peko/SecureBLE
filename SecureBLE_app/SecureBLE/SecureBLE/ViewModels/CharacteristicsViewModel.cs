using System.Collections.ObjectModel;
using Autofac;
using Plugin.BLE.Abstractions.Contracts;
using Xamarin.Forms;

namespace SecureBLE.ViewModels
{
    public class CharacteristicsViewModel : BaseViewModel
    {
        private ObservableCollection<ICharacteristic> _characteristics;

        public ObservableCollection<ICharacteristic> Characteristics
        {
            get { return _characteristics; }
            set
            {
                _characteristics = value;
                RaisePropertyChanged();
            }
        }

        private ICharacteristic _selectedCharacteristic;

        public ICharacteristic SelectedCharacteristic
        {
            get { return _selectedCharacteristic; }
            set
            {
                _selectedCharacteristic = value;
                RaisePropertyChanged();
                GoToNextView();
            }
        }

        public CharacteristicsViewModel(object selectedService)
        {
            Characteristics = new ObservableCollection<ICharacteristic>();
            SelectedCharacteristic = null;
            var service = (IService) selectedService;
            ShowCharacteristics(service);
        }

        private async void ShowCharacteristics(IService service)
        {
            if (service != null)
            {
                Characteristics = new ObservableCollection<ICharacteristic>(await service.GetCharacteristicsAsync());
            }
        }

        private async void GoToNextView()
        {
            if (SelectedCharacteristic == null || !SelectedCharacteristic.CanRead || !SelectedCharacteristic.CanWrite) return;
            var bootstrapper = new Startup.Bootstrapper();
            var container = bootstrapper.Bootstrap();
            await Application.Current.MainPage.Navigation.PushAsync(container.Resolve<Views.CommunicationView>(new NamedParameter("selectedCharacteristic", SelectedCharacteristic)));

        }
    }
}
