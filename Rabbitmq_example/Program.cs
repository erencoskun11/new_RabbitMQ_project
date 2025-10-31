using RabbitMQ.Client;
using System.Text;

//Created connection
ConnectionFactory factory = new();
factory.Uri = new("amqps://xcqfhxlm:PpulfA_CKKshVziwYWdxB-4-C02Ue2XP@codfish.rmq.cloudamqp.com/xcqfhxlm");


// Objects declared with using are automatically disposed when they go out of scope
using IConnection connection = factory.CreateConnection();
connection.CreateModel();
using IModel channel = connection.CreateModel();


//create queue

channel.QueueDeclare(queue: "example_queue",exclusive:false,autoDelete:false);

//send a message to queue

byte[] message =  Encoding.UTF8.GetBytes("hii");
channel.BasicPublish(exchange:"",routingKey :"example_queue",body:message);


Console.Read();
