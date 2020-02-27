using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Worker.Controllers
{

    public interface ICustomEventHandler
    {
        
    }
    
    public class CustomEventHandler : ICustomEventHandler
    {
        private readonly IRabbitMqService _rabbitMqService;

        public CustomEventHandler(IRabbitMqService rabbitMqService)
        {
            
            _rabbitMqService = rabbitMqService;
            var channel = _rabbitMqService.GetClient();
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += EventRecieved;
            channel.BasicConsume(queue: "hello",
                autoAck: true,
                consumer: consumer);
        }

        private void EventRecieved(object sender, BasicDeliverEventArgs e)
        {
            var body = e.Body;
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine("Event revieved with the following message: " + message);
        }
    }
}