namespace SecureBLE.Services.Interfaces
{
	public interface ICommunicationService
	{
		void Connect();
		void ReadMessage(string message);
	}
}
