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


//create queue
channel.QueueDeclare(queue: "example-queue", exclusive: false,durable:false);


//read message from queue 

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue:"example_queue",autoAck:false,consumer);
channel.BasicQos(0,2,false);

consumer.Received += (sender, e) =>
{
    //received the message
    //e.Body : kuyuktaki mesajin verisini getirecektir
    //e.Body.Span or e.Body.ToArray(): kuyruktakı mesajın byt verisini getirecektir
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
    channel.BasicAck(deliveryTag:e.DeliveryTag,multiple:false);
};

Console.Read();

