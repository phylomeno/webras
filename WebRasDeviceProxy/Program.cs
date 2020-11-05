using System;
using System.Text;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.Configuration;

namespace WebRasDeviceProxy
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Welcome to WebRas Device Proxy");

            IConfiguration configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables("WebRas_")
                .Build();

            var connectionString = BuildConnectionString(configuration);

            using (var deviceClient = DeviceClient.CreateFromConnectionString(connectionString, TransportType.Mqtt))
            {
                deviceClient.SendEventAsync(new Message(Encoding.UTF8.GetBytes("Successfully connected to IoT Hub")));
            }

            Console.ReadLine();
        }

        private static string BuildConnectionString(IConfiguration configuration)
        {
            var hostName = configuration["HostName"];
            var deviceId = configuration["DeviceId"];
            var key = configuration["Key"];

            var connectionString =
                $"HostName={hostName};DeviceId={deviceId};SharedAccessKey={key}";
            return connectionString;
        }
    }
}