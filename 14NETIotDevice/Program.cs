using System;
using StructureMap;
using System.Threading;
using _14NETIotDevice.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace _14NETIotDevice
{
    class Program
    {
        private static Container _container;
        static void Main(string[] args)
        {
            DependencyInjectionConfig.Configure();
            ServiceCollection services = new ServiceCollection();
            _container = new Container();
            _container.Configure(config =>
            {
                config.Scan(scan =>
                {
                    scan.AssemblyContainingType(typeof(Program));
                    scan.WithDefaultConventions();
                });

                config.Populate(services);
            });


            Console.WriteLine("IoT Hub Quickstarts - Simulated device. Ctrl-C to exit.\n");
            Process();
            Console.ReadLine();
        }

        private static void Process()
        {
            IDeviceMessageService deviceMessageServ = _container.GetInstance<IDeviceMessageService>();
            double v = 0;
            double vi = 0;
            double t = 0;
            double ti = 0;
            double a = 8.3;
            double temp = 90;
            double vkh = 0;

            while (true)
            {
                t = ti + t + 1;
                v = vi + a * t;

                if (vkh > 150) temp = temp + 5;

                vkh = v * 8;
                vkh = vkh / 5;

                string msg1 = $"Temperatura de {temp}ºC a {vkh} km/h";
                deviceMessageServ.SendMessage(msg1);
                Console.WriteLine(msg1);

                if (temp > 90)
                {
                    string msg2 = $"Atenção! A temperatura está anormal: {temp}ºC";
                    Console.WriteLine(msg2);
                    deviceMessageServ.SendMessage(msg2);
                } 

                if (temp == 130)
                {
                    string msg3 = $"O carro esquentou!";
                    deviceMessageServ.SendMessage(msg3);
                    Console.WriteLine(msg3);
                    break;
                }

                Thread.Sleep(1000);
            }
        }
    }
}
