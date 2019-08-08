using _14NETIotDevice.Services;
using _14NETIotDevice.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace _14NETIotDevice
{
    public class DependencyInjectionConfig
    {
        public static void Configure()
        {
            //setup our DI
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddSingleton<IConfigurationBuilder, ConfigurationBuilder>()
                .AddSingleton<IDeviceMessageService, DeviceMessageService>()
                .BuildServiceProvider();
        }
    }
}
