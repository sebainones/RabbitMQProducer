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

            string message = "Hello Seba!";
            if (args.Length != 0)
            {
                message = args[0];
            }
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: "helloQueue",
                                 basicProperties: null,
                                 body: body);
            Console.WriteLine(" [x] Sent {0}", message);
        }

        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();
    }
}