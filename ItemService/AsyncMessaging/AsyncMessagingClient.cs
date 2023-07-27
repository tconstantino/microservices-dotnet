using RabbitMQ.Client;

namespace ItemService.AsyncMessaging
{
    public class AsyncMessagingClient
    {
        private readonly IConfiguration _configuration;
        private readonly string _nomeDaFila;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public AsyncMessagingClient(IConfiguration configuration)
        {
            _configuration = configuration;

            var rabbitHost = _configuration["RabbitMQHost"];
            var rabbitPort = _configuration["RabbitMQPort"];

            _connection = new ConnectionFactory()
            {
                HostName = rabbitHost,
                Port = int.Parse(rabbitPort)
            }.CreateConnection();

            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
            _nomeDaFila = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(
                queue: _nomeDaFila, 
                exchange: 
                "trigger", routingKey: ""
                );
        }
    }
}
