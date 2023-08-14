using Confluent.Kafka;

namespace N5Company.Kafka
{
    public class KafkaProducer
    {
        private readonly ProducerConfig _config;
        private readonly IConfiguration _configuration;

        public KafkaProducer(IConfiguration configuration)
        {
            _config = new ProducerConfig
            {
                BootstrapServers = configuration["Kafka:BootstrapServers"]
            };
            _configuration = configuration;
        }

        public async Task PublishPermissionOperationAsync(string operationName)
        {
            using (var producer = new ProducerBuilder<Null, string>(_config).Build())
            {
                var message = new Message<Null, string>
                {
                    Value = $"{{ \"Id\": \"{Guid.NewGuid()}\", \"Name\": \"{operationName}\" }}"
                };

                var deliveryResult = await producer.ProduceAsync(_configuration["Kafka:Topic"], message);

                Console.WriteLine($"Mensaje enviado a Kafka. Offset: {deliveryResult.Offset}");
            }
        }
    }
}
