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
            while (true)
            {
                Console.WriteLine(">>> Enter a message which you want to send or type 'q' to exist app. <<<");
                var userMessage = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(userMessage))
                {
                    Console.WriteLine("You have to type a message.");
                    continue;
                }

                if (userMessage == "q")
                    return;

                Console.WriteLine("[Start]");
                try
                {
                    rabbitMQManager.SendMessage(QueueNames.HELLO_WORLD, userMessage);
                    Console.WriteLine("[Done]");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Something went wrong: {ex.Message}]");
                    Console.ReadKey();
                    return;
                }
            }

        }
    }
}