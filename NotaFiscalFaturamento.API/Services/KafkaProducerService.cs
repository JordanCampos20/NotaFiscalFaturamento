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
                BootstrapServers = Environment.GetEnvironmentVariable("CONEXAO_KAFKA"),
                SecurityProtocol = SecurityProtocol.Ssl,
                SaslMechanism = SaslMechanism.Plain,
                SaslUsername = Environment.GetEnvironmentVariable("USERNAME_KAFKA"),
                SaslPassword = Environment.GetEnvironmentVariable("PASSWORD_KAFKA"),
                MessageTimeoutMs = 45000,
                ClientId = Environment.GetEnvironmentVariable("CLIENTID_KAFKA")
            }).Build();

        private readonly string _topic = "validar-estoque";                    

        public async Task EnviarNota(NotaDTO nota)
        {
            var message = JsonSerializer.Serialize(nota);

            await _producer.ProduceAsync(_topic, new Message<string, string> { Key = Guid.NewGuid().ToString(), Value = message });
        }
    }
}
