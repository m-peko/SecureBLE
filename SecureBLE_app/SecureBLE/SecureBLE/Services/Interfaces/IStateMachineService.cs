using SecureBLE.Enums.Communication;
using SecureBLE.Enums.StateMachine;

namespace SecureBLE.Services.Interfaces
{
	public interface IStateMachineService
	{
		State OnReceive(State currentState, Event receivedEvent);
		Event MessageTypeToEvent(MessageType type);
	}
}
