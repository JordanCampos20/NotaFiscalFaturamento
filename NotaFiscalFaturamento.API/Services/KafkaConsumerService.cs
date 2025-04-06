using Confluent.Kafka;
using NotaFiscalFaturamento.API.DTOs;
using NotaFiscalFaturamento.API.Interfaces;
using NotaFiscalFaturamento.Domain.Enums;
using System.Text.Json;

namespace NotaFiscalFaturamento.API.Services
{
    public class KafkaConsumerService(IServiceScopeFactory serviceScopeFactory) : IKafkaConsumerService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;

        private readonly IConsumer<string, string> _consumer = new
            ConsumerBuilder<string, string>(new ConsumerConfig()
            {
                BootstrapServers = Environment.GetEnvironmentVariable("CONEXAO_KAFKA"),
                SecurityProtocol = SecurityProtocol.Ssl,
                SaslMechanism = SaslMechanism.Plain,
                SaslUsername = Environment.GetEnvironmentVariable("USERNAME_KAFKA"),
                SaslPassword = Environment.GetEnvironmentVariable("PASSWORD_KAFKA"),
                ClientId = Environment.GetEnvironmentVariable("CLIENTID_KAFKA"),
                GroupId = Environment.GetEnvironmentVariable("GROUPID_KAFKA")
            }).Build();

        private readonly string[] _topics = { "estoque-validado", "estoque-insuficiente" };

        public void ConsumirNotas()
        {
            using (IServiceScope scope = _serviceScopeFactory.CreateScope())
            {
                IFaturamentoService _faturamentoService = scope.ServiceProvider.GetRequiredService<IFaturamentoService>();

                IKafkaProducerService _kafkaProducerService = scope.ServiceProvider.GetRequiredService<IKafkaProducerService>();

                _consumer.Subscribe(_topics);

                try
                {
                    while (true)
                    {
                        var consume = _consumer.Consume(CancellationToken.None);

                        var notaEstoque = JsonSerializer.Deserialize<NotaEstoqueDTO>(consume.Message.Value);

                        if (notaEstoque == null)
                            continue;

                        Thread.Sleep(10000);

                        StatusEnum status = consume.Topic == "estoque-validado" ? StatusEnum.Aprovada : StatusEnum.Rejeitada;

                        _faturamentoService.AtualizarStatusNota(notaEstoque.NotaId, (int)status);

                        _consumer.Commit(consume);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro no Consumer: {ex.Message}");
                }
            }
        }
    }
}
