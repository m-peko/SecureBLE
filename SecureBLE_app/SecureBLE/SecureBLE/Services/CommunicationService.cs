using System;
using System.Text;
using System.Threading.Tasks;
using Plugin.BLE.Abstractions.Contracts;
using SecureBLE.Enums.Communication;
using SecureBLE.Services.Interfaces;

namespace SecureBLE.Services
{
    public class CommunicationService : ICommunicationService
    {
        public async Task WriteMessageToArduino(ICharacteristic characteristic, MessageType type, string content = "")
        {
            var message = ParseMessage(type, content);
            var byteArray = Encoding.ASCII.GetBytes(message);
            await characteristic.WriteAsync(byteArray);

        }

		public void ReadMessage(string message)
		{
			MessageType messageType = ExtractMessageTypeEnum(message);
			string messageContent = ExtractMessageContent(message);

			switch (messageType)
			{
				case MessageType.PU:
					/* TODO: keys generation
                     * save Arduino public key
                     * generate Android public key
                     * send Android public key to Arduino
                     * set state to shared key generation
                     * if successfull send success to Arduino
                     */
					break;
				case MessageType.SUCCESS:
					/* TODO: data transfer
                     * set state to encrypted connection
                     */
					break;

				case MessageType.DATA:
				/* TODO: read data from Arduino
				 * show data on screen
				 */
				case MessageType.FAILURE:
				case MessageType.RESET:
					/* TODO: end communication
                     * set state to start
                     */
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private string ParseMessage(MessageType type, string messageContent = "")
		{
			if (messageContent == "")
				return $"${type.ToString()}=;";

			return $"${type.ToString()}={messageContent};";
		}

		private MessageType ExtractMessageTypeEnum(string message)
		{
			var position = message.IndexOf("=", StringComparison.Ordinal);
			if (position == -1) position = message.IndexOf(";", StringComparison.Ordinal);
			var type = message.Substring(1, position - 1);

			if (Enum.TryParse(type, out MessageType messageTypeEnum))
				return messageTypeEnum;
			return MessageType.RESET;
		}

		private string ExtractMessageContent(string message)
		{
			var first = message.IndexOf("=", StringComparison.Ordinal);
			if (first == -1) return "";
			var last = message.IndexOf(";", StringComparison.Ordinal);
			var content = message.Substring(first + 1, last - first - 1);
			return content;
		}
	}
}
