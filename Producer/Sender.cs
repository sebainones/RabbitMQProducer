using RabbitMQ.Client;
using System.Text;
public class Sender
{
    public static void Main(String[] args)
    {

        var factory = new ConnectionFactory();

        Console.WriteLine($"About to connect to host :{factory.HostName}");

        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "helloQueue",
                              durable: false,
                              exclusive: false,
                              autoDelete: false,
                              arguments: null);
            var message = GetMessage(args);

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: "helloQueue",
                                 basicProperties: null,
                                 body: body);
            Console.WriteLine(" [x] Sent {0}", message);
        }

        // Console.WriteLine(" Press [enter] to exit.");
        // Console.ReadLine();
    }

    private static string GetMessage(string[] args)
    {
        return ((args.Length > 0) ? string.Join(" ", args) : "Hello Sebas!");
    }
}