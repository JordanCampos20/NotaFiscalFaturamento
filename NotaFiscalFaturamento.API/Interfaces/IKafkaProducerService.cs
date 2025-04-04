using NotaFiscalFaturamento.Application.DTOs;

namespace NotaFiscalFaturamento.API.Interfaces
{
    public interface IKafkaProducerService
    {
        Task EnviarNota(NotaDTO nota);
    }
}
