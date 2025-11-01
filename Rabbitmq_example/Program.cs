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

#region P2P (Point-to-Point) Tasarimi
string queueName = "example-p2p-queue";

channel.QueueDeclare(
    queue : queueName,
    durable : false,
    exclusive : false,
    autoDelete:false);

byte[] message = Encoding.UTF8.GetBytes("merhaba");
channel.BasicPublish(
    exchange:string.Empty,
    routingKey:queueName,
    body: message
    );
#endregion

#region Publish/Subscrive (Pub/Sub) Tasarimi

string exchangeName = "example-pub-sub-exchange";

channel.ExchangeDeclare(
    exchange: exchangeName,
    type:ExchangeType.Fanout);

for (int i = 0; i < 100; i++)
{
    byte[] message2 = Encoding.UTF8.GetBytes(i +"merhaba");

    channel.BasicPublish(
        exchange: exchangeName,
        routingKey: string.Empty,
        body: message2);
}
#endregion

#region Work Queue(Is kuyrugu) tasarimi 
string queueName3 = "example_work_queue";

channel.QueueDeclare(
    queue:queueName3,
    durable:false,
    exclusive:false,
    autoDelete:false
    );
for (int i = 0;i < 100;i++)
{
    await Task.Delay(200);
    byte[] message3= Encoding.UTF8.GetBytes(i + "hii");

    channel.BasicPublish(
        exchange: exchangeName,
        routingKey: queueName3,
        body: message3
        );
}
#endregion


Console.Read();







