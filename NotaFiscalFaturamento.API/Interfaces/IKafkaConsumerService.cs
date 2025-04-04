using NotaFiscalFaturamento.Application.DTOs;

namespace NotaFiscalFaturamento.API.Interfaces
{
    public interface IKafkaConsumerService
    {
        void ConsumirNotas();
    }
}
