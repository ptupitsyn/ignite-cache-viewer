using System;
using System.Threading;
using Apache.Ignite.Core;
using Apache.Ignite.Core.Discovery.Tcp;
using Apache.Ignite.Core.Discovery.Tcp.Static;

namespace IgniteCacheViewer.TestServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var cfg = new IgniteConfiguration
            {
                DiscoverySpi = new TcpDiscoverySpi
                {
                    IpFinder = new TcpDiscoveryStaticIpFinder
                    {
                        Endpoints = new[] {"127.0.0.1:47500"}
                    },
                    SocketTimeout = TimeSpan.FromSeconds(0.3)
                }
            };

            var ignite = Ignition.Start(cfg);

            var persons = ignite.GetOrCreateCache<int, Person>("persons");

            persons[1] = new Person {Id = 1, Name = "Vasya"};
            persons[2] = new Person {Id = 2, Name = "Petya"};

            var strings = ignite.GetOrCreateCache<int, string>("strings");
            strings[1] = "Hello, World!";

            Thread.Sleep(Timeout.Infinite);
        }
    }

    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
