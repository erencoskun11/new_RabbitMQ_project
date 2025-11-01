using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

//Created connection
ConnectionFactory factory = new();
factory.Uri = new("amqps://xcqfhxlm:PpulfA_CKKshVziwYWdxB-4-C02Ue2XP@codfish.rmq.cloudamqp.com/xcqfhxlm");


// Objects declared with using are automatically disposed when they go out of scope
using IConnection connection = factory.CreateConnection();
connection.CreateModel();
using IModel channel = connection.CreateModel();

//1. step
channel.ExchangeDeclare(exchange: "direct_exchange_example", type: ExchangeType.Direct);

//2. step
string queueName = channel.QueueDeclare().QueueName;

//3. step
channel.QueueBind(queue:queueName,
    exchange:"direct_exchange_example",
    routingKey:"direct_queue_example"
    );

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: queueName, autoAck:true,consumer: consumer);

consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);

};





Console.Read();







//first step : exchange name must be same as one in publisher

//second step : 

