using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Worker.Controllers {
    [ApiController]
    [Route ("")]
    public class WeatherForecastController : ControllerBase {
        private static readonly string[] Summaries = new [] {
            "Freezing",
            "Bracing",
            "Chilly",
            "Cool",
            "Mild",
            "Warm",
            "Balmy",
            "Hot",
            "Sweltering",
            "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController (ILogger<WeatherForecastController> logger) {
            _logger = logger;
        }

        [HttpGet]
        public async Task Get () {
            Console.WriteLine ("WORKER");

            ConnectionFactory factory = new ConnectionFactory () { HostName = "localhost", Port = 5672 };
            factory.UserName = "guest";
            factory.Password = "guest";
            IConnection conn = factory.CreateConnection ();
            IModel channel = conn.CreateModel ();
            channel.QueueDeclare (queue: "hello",
                durable : false,
                exclusive : false,
                autoDelete : false,
                arguments : null);

            var consumer = new EventingBasicConsumer (channel);
            consumer.Received += (model, ea) => {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString (body);
                Console.WriteLine (" [x] Received from Rabbit: {0}", message);
            };
            channel.BasicConsume (queue: "hello",
                autoAck : true,
                consumer : consumer);
        }
    }
}