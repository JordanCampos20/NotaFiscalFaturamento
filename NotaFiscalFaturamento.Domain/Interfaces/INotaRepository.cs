using NotaFiscalFaturamento.Domain.Entities;

namespace NotaFiscalFaturamento.Domain.Interfaces
{
    public interface INotaRepository
    {
        IEnumerable<Nota> GetNotas();
        Nota? GetById(int id);
        Nota? Create(Nota nota);
        Nota? Update(int id, Nota nota);
        bool Remove(int id);
    }
}
