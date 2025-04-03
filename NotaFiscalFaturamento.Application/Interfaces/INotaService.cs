using NotaFiscalFaturamento.Application.DTOs;

namespace NotaFiscalFaturamento.Application.Interfaces
{
    public interface INotaService
    {
        IEnumerable<NotaDTO> GetNotas();
        NotaDTO? GetById(int id);
        NotaDTO? Create(NotaDTO notaDTO);
        NotaDTO? Update(int id, NotaDTO notaDTO);
        bool Remove(int id);
    }
}
