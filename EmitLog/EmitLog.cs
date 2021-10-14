using System;
using RabbitMQ.Client;
using System.Text;
using System.Threading;

class EmitLog
{
  public static void Main(string[] args)
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

      while (true) {
        var message = GetMessage(args);
        var body = Encoding.UTF8.GetBytes(message);
        channel.BasicPublish(exchange: "logs", routingKey: "", basicProperties: null, body: body);
        Console.WriteLine(" [x] Sent {0}", message);
        Thread.Sleep(1000);
      }
    }
  }

  private static string GetMessage(string[] args)
  {
    return ((args.Length > 0) ? string.Join(" ", args) : "info: Hello World!");
  }
}
