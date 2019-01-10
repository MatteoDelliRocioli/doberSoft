using System;
using RabbitMQ.Client;
using System.Text;

// https://www.rabbitmq.com/tutorials/tutorial-one-dotnet.html

namespace doberSoft.AMQP.send
{
    class Program
    {
        public static void Main()
        {
            var factory = new ConnectionFactory() { HostName = "sheep.rmq.cloudamqp.com" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);

                string message = "Hello World!";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "", routingKey: "hello", basicProperties: null, body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
