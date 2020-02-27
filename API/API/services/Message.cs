using System;
using System.Text;
using RabbitMQ.Client;

namespace API.services {
    public interface IMessage {
        bool Enqueue (string message);
    }

    public class Message : IMessage {
        ConnectionFactory _factory;
        IConnection _conn;
        IModel _channel;

        public Message () {
            Console.WriteLine ("about to connect to rabbit");

            _factory = new ConnectionFactory () { HostName = "localhost", Port = 5672 };
            _factory.UserName = "guest";
            _factory.Password = "guest";
            _conn = _factory.CreateConnection ();
            _channel = _conn.CreateModel ();
            _channel.QueueDeclare (queue: "hello",
                durable : false,
                exclusive : false,
                autoDelete : false,
                arguments : null);

            Console.WriteLine ("connected?");
        }
        public bool Enqueue (string message) {
            var body = Encoding.UTF8.GetBytes ("server processed " + message);
            _channel.BasicPublish (exchange: "",
                routingKey: "hello",
                basicProperties : null,
                body : body);
            Console.WriteLine (" [x] Sent {0}", message);
            return true;
        }
    }
}