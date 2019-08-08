using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using _14NETIotDevice.Interfaces;
using Microsoft.Azure.Devices.Client;

namespace _14NETIotDevice.Services
{
    public class DeviceMessageService : IDeviceMessageService
    {
        public DeviceMessageService()
        {
            _deviceClient = DeviceClient.CreateFromConnectionString(_connectionString, TransportType.Mqtt);
        }

        private static DeviceClient _deviceClient;
        private const string _connectionString = "HostName=HUBIOT14NET.azure-devices.net;DeviceId=14NETIOTEDGEDEVICE;SharedAccessKey=aZm7Oz3MR2rXUYDp54JBrWH11vqJnyq2dEb6OKjI7W8=";

        public async void SendMessage(string text)
        {
            string messageString = JsonConvert.SerializeObject(text);
            Message message = new Message(Encoding.ASCII.GetBytes(messageString));
            await _deviceClient.SendEventAsync(message).ConfigureAwait(false);
            await Task.Delay(1000).ConfigureAwait(false);
        }
    }
}
