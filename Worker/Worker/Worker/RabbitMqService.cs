using RabbitMQ.Client;

namespace Worker
{
    public interface IRabbitMqService
    {
        IModel GetClient();
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
    }
}