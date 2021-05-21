using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Shared
{
    public class RabbitMqManager
    {
        public ConnectionFactory Factory { get; }

        public RabbitMqManager(string hostName)
        {
            Factory = new ConnectionFactory
            {
                HostName = hostName
            };
        }

        public void SendMessage(string queue, string message)
        {
            using (var connection = Factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queue, durable: false,
                    exclusive: false, autoDelete: false, arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "", routingKey: queue, basicProperties: null, body: body);

                Console.WriteLine($"The message: '{message}' - sent.");
            }
        }

        public void SubscribeQueue(IModel channel, string queue, Action<string> received)
        {
            channel.QueueDeclare(queue: queue, durable: false,
                exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                received(message);
            };

            channel.BasicConsume(queue, autoAck: true, consumer);
        }
    }
}