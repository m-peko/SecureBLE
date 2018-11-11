using System;
using ArduinoApp.Enums;
using ArduinoApp.Serivces.Interfaces;

namespace ArduinoApp.Serivces
{
    public class StateMachineService : IStateMachineService
    {
        public State GoToNextState(State currentState, Event eEvent)
        {
            switch (currentState)
            {
                case State.Start:
                    if (eEvent == Event.ConnectRequest)
                        return State.KeysGeneration;
                    return currentState;
                case State.KeysGeneration:
                    if (eEvent == Event.PublicKeyReceived)
                        return State.SharedSecretGeneration;
                    return State.Start;
                case State.SharedSecretGeneration:
                    if (eEvent == Event.SharedSecretSuccess)
                        return State.EncryptedConnection;
                    return State.Start;
                case State.EncryptedConnection:
                    return State.Start;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public Event ConvertMessageTypeToEvent(MessageType type)
        {
            switch (type)
            {
                case MessageType.CONNECT:
                    return Event.ConnectRequest;
                case MessageType.PU:
                    return Event.PublicKeyReceived;
                case MessageType.SUCCESS:
                    return Event.SharedSecretSuccess;
                case MessageType.FAILURE:
                    return Event.SharedSecretFailure;
                case MessageType.RESET:
                    return Event.Reset;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
