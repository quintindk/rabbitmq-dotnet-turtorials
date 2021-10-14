using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading;

class ReceiveLogsTopic
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
      channel.ExchangeDeclare(exchange: "topic_logs", type: "topic");
      // declare a server-named queue
      var queueName = channel.QueueDeclare(queue: "").QueueName;

      if (args.Length < 1)
      {
        Console.Error.WriteLine("Usage: {0} [binding_key...]", Environment.GetCommandLineArgs()[0]);
        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();
        Environment.ExitCode = 1;
        return;
      }

      foreach (var bindingKey in args)
      {
        channel.QueueBind(queue: queueName, exchange: "topic_logs", routingKey: bindingKey);
      }

      Console.WriteLine(" [*] Waiting for messages. To exit press CTRL+C");

      var consumer = new EventingBasicConsumer(channel);
      consumer.Received += (model, ea) =>
      {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        var routingKey = ea.RoutingKey;
        Console.WriteLine(" [x] Received '{0}':'{1}'", routingKey, message);
      };
      channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

      Thread.Sleep(Timeout.Infinite);
    }
  }
}
