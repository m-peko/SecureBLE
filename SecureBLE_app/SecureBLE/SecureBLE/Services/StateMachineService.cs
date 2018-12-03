using System.ComponentModel;
using SecureBLE.Enums.Communication;
using SecureBLE.Enums.StateMachine;
using SecureBLE.Services.Interfaces;

namespace SecureBLE.Services
{
    public class StateMachineService : IStateMachineService
    {
		public State OnReceive(State currentState, Event receivedEvent)
		{
			switch (currentState)
			{
				case State.STATE_START:
					if (Event.EVENT_CONNECT_REQUEST == receivedEvent)
					{
						return State.STATE_KEYS_GENERATION;
					}
					return currentState;
				case State.STATE_KEYS_GENERATION:
					if (Event.EVENT_PUBLIC_KEY_RECEIVED == receivedEvent)
					{
						return State.STATE_SHARED_SECRET_GENERATION;
					}
					return State.STATE_START;
				case State.STATE_SHARED_SECRET_GENERATION:
					if (Event.EVENT_SHARED_SECRET_SUCCESS == receivedEvent)
					{
						return State.STATE_ENCRYPTED_CONNECTION;
					}
					return State.STATE_START;
				case State.STATE_ENCRYPTED_CONNECTION:
					return State.STATE_START;
				default:
					throw new InvalidEnumArgumentException("Unknown state machine state.");
			}
		}

		public Event MessageTypeToEvent(MessageType type)
		{
			switch (type)
			{
				case MessageType.CONNECT:
					return Event.EVENT_CONNECT_REQUEST;
				case MessageType.PU:
					return Event.EVENT_PUBLIC_KEY_RECEIVED;
				case MessageType.SUCCESS:
					return Event.EVENT_SHARED_SECRET_SUCCESS;
				case MessageType.FAILURE:
					return Event.EVENT_SHARED_SECRET_FAILURE;
				case MessageType.RESET:
					return Event.EVENT_RESET;
				default:
					throw new InvalidEnumArgumentException("Unknown message type.");
			}
		}
	}
}
