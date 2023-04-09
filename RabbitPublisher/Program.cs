using RabbitMQ.Client;  
using System.Text;  
  
ConnectionFactory factory = new()  
{  
    Uri = new Uri("amqp://myuser:mypassword@localhost:5672/myvhost"),  
    ClientProvidedName = "Rabbit Publisher App"  
};  
var cnn = factory.CreateConnection();  
var channel = cnn.CreateModel();  
  
const string exchangeName = "SampleExchange";  
const string routingKey = "sample-routing-key";  
const string queueName = "SampleQueue";  
  
channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);  
channel.QueueDeclare(queueName, false, false, false, null);  
channel.QueueBind(queueName, exchangeName, routingKey, null);

for (var i = 0; i < 100; i++)
{
    var messageBodyBytes = Encoding.UTF8.GetBytes($"Message {i}");  
    channel.BasicPublish(exchangeName, routingKey, null, messageBodyBytes);
    Console.WriteLine($"Publishing message: Message ${i}");
    await Task.Delay(1000);
}

channel.Close();  
cnn.Close();