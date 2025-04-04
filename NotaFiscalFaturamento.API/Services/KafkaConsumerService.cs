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
                GroupId = "faturamento-service",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
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

                        StatusEnum status = consume.Topic == "estoque-validado" ? StatusEnum.Aprovada : StatusEnum.Rejeitada;

                        _faturamentoService.AtualizarStatusNota(notaEstoque.NotaId, (int)status);

                        Console.WriteLine($"📦 Nota {notaEstoque.NotaId} agora tem status: {status}");
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
