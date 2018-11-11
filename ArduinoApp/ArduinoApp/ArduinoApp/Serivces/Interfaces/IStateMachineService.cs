using ArduinoApp.Enums;

namespace ArduinoApp.Serivces.Interfaces
{
    public interface IStateMachineService
    {
        State GoToNextState(State currentState, Event eEvent);
        Event ConvertMessageTypeToEvent(MessageType type);
    }
}
