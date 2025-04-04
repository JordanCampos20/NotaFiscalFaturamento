namespace NotaFiscalFaturamento.API.Interfaces
{
    public interface IFaturamentoService
    {
        void AtualizarStatusNota(int NotaId, int Status);
    }
}
