using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Text;

namespace Customer.Microservice.Services
{
    public class Broker : IBroker
    {
        ConnectionFactory _factory;
        IConnection _conn;
        IModel _channel;
        private readonly ILogger<Broker> _logger;

        public Broker(ILogger<Broker> logger)
        {
            try
            {
            _logger = logger;

            logger.Log(LogLevel.Information,"about to connect to rabbit");

            _factory = new ConnectionFactory() { HostName = "rabbitmq", Port = 5672 };
            _factory.UserName = "guest";
            _factory.Password = "guest";
            _conn = _factory.CreateConnection();
            _channel = _conn.CreateModel();
            _channel.QueueDeclare(queue: "hello",
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Information, $"ERRORE => {ex.Message}");
            }
        }
        public bool Enqueue(string messageString)
        {
            var body = Encoding.UTF8.GetBytes("server processed " + messageString);
            _channel.BasicPublish(exchange: "",
                                routingKey: "hello",
                                basicProperties: null,
                                body: body);
            _logger.Log(LogLevel.Information, " [x] Published {0} to RabbitMQ", messageString);
            return true;
        }
    }
}
