using Confluent.Kafka;
using NotaFiscalFaturamento.API.Interfaces;
using NotaFiscalFaturamento.Application.DTOs;
using System.Text.Json;

namespace NotaFiscalFaturamento.API.Services
{
    public class KafkaProducerService : IKafkaProducerService
    {
        private readonly IProducer<string, string> _producer = new
            ProducerBuilder<string, string>(new ProducerConfig()
            {
                BootstrapServers = "localhost:9092"
            }).Build();

        private readonly string _topic = "validar-estoque";                    

        public async Task EnviarNota(NotaDTO nota)
        {
            var message = JsonSerializer.Serialize(nota);

            await _producer.ProduceAsync(_topic, new Message<string, string> { Key = Guid.NewGuid().ToString(), Value = message });
        }
    }
}
