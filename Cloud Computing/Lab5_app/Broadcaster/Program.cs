using Shared;
using System;

namespace Broadcaster
{
    class Program
    {
        private static void Main()
        {
            var hostName = "localhost";
            var rabbitMQManager = new RabbitMqManager(hostName);
            var newMessage = false;
            var connection = rabbitMQManager.Factory.CreateConnection();
            var channel = connection.CreateModel();
            rabbitMQManager.SubscribeQueue(channel, QueueNames.HELLO_MIKE,
                (message) =>
                {
                    Console.WriteLine($">>> User message: '{message}' <<<");
                    newMessage = true;
                });
            while (true)
            {
                Console.WriteLine(">>> Type 'q' to exist app or any to continue. <<<");
                var userMessage = Console.ReadLine();

                if (userMessage == "q")
                    return;

                Console.WriteLine("[Start]");

                rabbitMQManager.SendMessage(QueueNames.HELLO_WORLD, "Witaj użytkowniku, przestaw się");
                while (!newMessage)
                {
                }

                newMessage = false;
                Console.WriteLine("[Done]");
            }
        }
    }
}