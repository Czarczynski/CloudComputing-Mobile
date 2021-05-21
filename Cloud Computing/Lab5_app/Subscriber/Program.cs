using System;
using Shared;

namespace Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = "localhost";
            var queue = QueueNames.HELLO_WORLD;

            Console.WriteLine("Welcome in ClientOne application. To quit press any key.");

            var rabbitMQManager = new RabbitMqManager(host);

            using (var connection = rabbitMQManager.Factory.CreateConnection())
            using(var channel = connection.CreateModel())
            {
                rabbitMQManager.SubscribeQueue(channel, queue, (message) =>
                {
                    Console.WriteLine($">>> Received message: '{message}' <<<");
                });

                Console.ReadKey();
            }
          
        }
    }
}