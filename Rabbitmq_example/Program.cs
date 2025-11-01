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

channel.ExchangeDeclare(exchange: "fanout_exchange_example",
    type:ExchangeType.Fanout);

for(int i=0;i<100;i++)
{
    await Task.Delay(200);
    byte[] message = Encoding.UTF8.GetBytes($"hello {i}");
    channel.BasicPublish(
        exchange: "fanout_exchange_example",
        routingKey: string.Empty,
        body:message);
}



Console.Read();







