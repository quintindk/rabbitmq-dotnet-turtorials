using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading;

class ReceiveLogs
{
  public static void Main()
  {
    var hostname = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "localhost";
    var username = Environment.GetEnvironmentVariable("RABBITMQ_USERNAME") ?? "guest";
    var password = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD") ?? "guest";
    var vhost = Environment.GetEnvironmentVariable("RABBITMQ_VHOST") ?? "/";

    Console.WriteLine($"RABBITMQ_HOST:{hostname}");
    Console.WriteLine($"RABBITMQ_USERNAME:{username}");
    Console.WriteLine($"RABBITMQ_PASSWORD:***");
    Console.WriteLine($"RABBITMQ_VHOST:{vhost}");

    var factory = new ConnectionFactory()
    {
      HostName = hostname,
      UserName = username,
      Password = password,
      VirtualHost = vhost
    };

    using (var connection = factory.CreateConnection())
    using (var channel = connection.CreateModel())
    {
      channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);

      // declare a server-named queue
      var queueName = channel.QueueDeclare(queue: "").QueueName;
      channel.QueueBind(queue: queueName, exchange: "logs", routingKey: "");

      Console.WriteLine(" [*] Waiting for logs.");

      var consumer = new EventingBasicConsumer(channel);
      consumer.Received += (model, ea) =>
      {
        byte[] body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine(" [x] {0}", message);
      };
      channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

      Thread.Sleep(Timeout.Infinite);
    }
  }
}
