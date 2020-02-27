using System;
using System.Text;
using RabbitMQ.Client;

namespace Worker
{
    public interface IRabbitMqService
    {
        IModel GetClient();
        void publish(string customEvent);
    }
    
    public class RabbitMqService : IRabbitMqService
    {
        public IModel channel { get; set; }
        public RabbitMqService()
        {
            ConnectionFactory factory = new ConnectionFactory () { HostName = "rabbitmq", Port = 5672 };
            factory.UserName = "guest";
            factory.Password = "guest";
            IConnection conn = factory.CreateConnection ();
            channel = conn.CreateModel ();
            channel.QueueDeclare (queue: "hello",
                durable : false,
                exclusive : false,
                autoDelete : false,
                arguments : null);
        }


        public IModel GetClient()
        {
            return channel;
        }
        
        public void publish(string customEvent)
        {
            var body = Encoding.UTF8.GetBytes ("server processed " + customEvent);
            this.channel.BasicPublish (exchange: "",
                routingKey: "hello",
                basicProperties : null,
                body : body);
            Console.WriteLine (" [x] Sent {0}", customEvent);
        }
    }
}