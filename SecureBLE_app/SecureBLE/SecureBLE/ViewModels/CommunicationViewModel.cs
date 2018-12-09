using System.Collections.ObjectModel;
using System.Text;
using Plugin.BLE.Abstractions.Contracts;
using SecureBLE.Enums.Communication;
using SecureBLE.Enums.StateMachine;
using SecureBLE.Services.Interfaces;

namespace SecureBLE.ViewModels
{
    public class CommunicationViewModel : BaseViewModel
    {
        private State _state = State.STATE_START;
        private readonly ICommunicationService _communicationService;

        private ObservableCollection<Message> _messages;

        public ObservableCollection<Message> Messages
        {
            get { return _messages; }
            set { _messages = value; RaisePropertyChanged();}
        }

        public CommunicationViewModel(object selectedCharacteristic, ICommunicationService communicationService)
        {
            _communicationService = communicationService;

            Messages = new ObservableCollection<Message>();

            InitiateCommunicationWithArduino((ICharacteristic)selectedCharacteristic);
        }

        private async void InitiateCommunicationWithArduino(ICharacteristic selectedCharacteristic)
        {
            Messages.Add(new Message
            {
                Description = "Sending to Arduino",
                MessageType = MessageType.CONNECT
            });

            //TEST THIS!
            await _communicationService.WriteMessageToArduino(selectedCharacteristic, MessageType.CONNECT);
            var byteArray = await selectedCharacteristic.ReadAsync();
            var message = Encoding.ASCII.GetString(byteArray);
            /*
             * on vrati public key - next state generate public key i posalji mu nakon toga next state i generate shared key...
             * refactor communication and statemachine services
             */
        }
    }

    public class Message
    {
        public MessageType MessageType;
        public string MessageContent;
        public string Description;
    }
}
