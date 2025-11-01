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

channel.ExchangeDeclare(exchange:"direct_exchange_example",type: ExchangeType.Direct);

while (true)
{
    Console.Write("message : ");
    string message = Console.ReadLine();
    byte[] byteMessage= Encoding.UTF8.GetBytes(message);

    channel.BasicPublish(
        exchange:"direct_exchange_example",
        routingKey:"direct_queue_example",
        body : byteMessage);


}



Console.Read();

