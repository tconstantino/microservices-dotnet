using ItemService.EventProcess;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ItemService.AsyncMessaging
{
    public class AsyncMessagingClient : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly string _nomeDaFila;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IEventProcessor _eventProcessor;

        public AsyncMessagingClient(IConfiguration configuration, IEventProcessor eventProcessor)
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

            _eventProcessor = eventProcessor;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumidor = new EventingBasicConsumer(_channel);
            consumidor.Received += (ModuleHandle, eventArgs) =>
            {
                ReadOnlyMemory<byte> body = eventArgs.Body;
                string? mensagem = Encoding.UTF8.GetString(body.ToArray());
                _eventProcessor.ProcessarEventoRestaurante(mensagem);
            };

            _channel.BasicConsume(queue: _nomeDaFila, autoAck: true, consumer: consumidor);
            
            return Task.CompletedTask;
        }
    }
}
