using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new()  
{  
    Uri = new Uri("amqp://myuser:mypassword@localhost:5672/myvhost"),  
    ClientProvidedName = "Rabbit Consumer App 2"  
};  
var cnn = factory.CreateConnection();  
var channel = cnn.CreateModel();

const string queueName = "SampleQueue";

channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (sender, args) =>
{
    Task.Delay(1000);
    var body = args.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"Message consumed: {message}");
    channel.BasicAck(args.DeliveryTag, multiple: false);
};

var consumerTag = channel.BasicConsume(queueName, autoAck: false, consumer);

Console.ReadKey();
channel.BasicCancel(consumerTag);
channel.Close();
cnn.Close();