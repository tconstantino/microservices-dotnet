using RabbitMQ.Client;
using RestauranteService.Dtos;

namespace RestauranteService.AsyncMessaging
{
    public class AsyncMessagingClient : IAsyncMessagingClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public AsyncMessagingClient(IConfiguration configuration)
        {
            _configuration = configuration;

            var rabbitHost = _configuration["RabbitMQHost"];
            var rabbitPort = _configuration["RabbitMQPort"];

            _connection = new ConnectionFactory() { 
                HostName = rabbitHost, 
                Port = int.Parse(rabbitPort)
            }.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
        }
        public void PublicarRestaurante(RestauranteCreateDto restauranteCreateDto)
        {
            throw new NotImplementedException();
        }
    }
}
