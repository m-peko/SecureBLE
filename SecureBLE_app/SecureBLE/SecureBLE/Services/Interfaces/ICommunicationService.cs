using System.Threading.Tasks;
using Plugin.BLE.Abstractions.Contracts;
using SecureBLE.Enums.Communication;

namespace SecureBLE.Services.Interfaces
{
	public interface ICommunicationService
	{
	    Task WriteMessageToArduino(ICharacteristic characteristic, MessageType type, string content = "");
		void ReadMessage(string message);
	}
}
