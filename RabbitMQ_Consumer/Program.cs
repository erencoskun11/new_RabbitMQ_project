using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Diagnostics;
using System.Text;


// create connection 


ConnectionFactory factory = new();

factory.Uri = new("amqps://xcqfhxlm:PpulfA_CKKshVziwYWdxB-4-C02Ue2XP@codfish.rmq.cloudamqp.com/xcqfhxlm");

//create canal active connection
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "fanout_exchange_example",
    type:ExchangeType.Fanout
    );
Console.WriteLine("enter the queue name");
string _queueName = Console.ReadLine();

channel.QueueDeclare(
    queue:_queueName,
    exclusive : false);

channel.QueueBind(
    queue: _queueName,
    exchange: "fanout_exchange_example",
    routingKey: string.Empty

    );

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(
    queue : _queueName,
    autoAck : true,
    consumer: consumer);

consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};




Console.Read();

