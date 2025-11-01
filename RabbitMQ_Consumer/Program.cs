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

#region P2P (Point-to-Point) Tasarimi
string queueName = "example-p2p-queue";

channel.QueueDeclare(
    queue: queueName,
    durable: false,
    exclusive: false,
    autoDelete: false
    );

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(
    queue: queueName,
    autoAck:false,
    consumer : consumer);

consumer.Received += (sender, e) =>
{
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};

#endregion

#region Publish/Subscribe (Pub/Sub) Tasarimi
string exchangeName = "example_pub_sub_exchange";

channel.ExchangeDeclare(
    exchange: exchangeName,
    type: ExchangeType.Fanout);

string queueName2 = channel.QueueDeclare().QueueName;

channel.QueueBind(
    queue: queueName2,
    exchange: exchangeName,
    routingKey: string.Empty
    );

EventingBasicConsumer consumer2 = new(channel);
channel.BasicConsume(
    queue: queueName2,
    autoAck:false,
    consumer : consumer2
    );

consumer.Received += (sender, e) =>
{
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};
#endregion

#region Work Queue(is kuyrugu) tasarimi 

string queueName3 = "example_work_queue";

channel.QueueDeclare(
    queue: queueName3,
    durable:false,
    exclusive:false,
    autoDelete:false
    );

EventingBasicConsumer consumer3 = new(channel);

channel.BasicConsume(
    queue: queueName3,
    autoAck:true,
    consumer : consumer3);

channel.BasicQos(
    prefetchCount: 1,
    prefetchSize:0,
    global:false

    );



#endregion





Console.Read();