using System;
using System.Linq;
using RabbitMQ.Client;
using System.Text;
using System.Threading;

class EmitLogDirect
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
      channel.ExchangeDeclare(exchange: "direct_logs", type: "direct");

      while (true)
      {
        var severity = (args.Length > 0) ? args[0] : "info";
        var message = (args.Length > 1) ? string.Join(" ", args.Skip(1).ToArray()) : "Hello World!";
        var body = Encoding.UTF8.GetBytes(message);
        channel.BasicPublish(exchange: "direct_logs", routingKey: severity, basicProperties: null, body: body);
        Console.WriteLine(" [x] Sent '{0}':'{1}'", severity, message);
        Thread.Sleep(1000);
      }
    }
  }
}
