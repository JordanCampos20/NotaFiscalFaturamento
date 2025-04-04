using NotaFiscalFaturamento.API.Interfaces;
using NotaFiscalFaturamento.Application.DTOs;
using NotaFiscalFaturamento.Application.Interfaces;

namespace NotaFiscalFaturamento.API.Services
{
    public class FaturamentoService(IServiceScopeFactory serviceScopeFactory) : IFaturamentoService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;
        
        public void AtualizarStatusNota(int NotaId, int Status)
        {
            using (IServiceScope scope = _serviceScopeFactory.CreateScope())
            {
                INotaService _notaService = scope.ServiceProvider.GetRequiredService<INotaService>();

                NotaDTO? nota = _notaService.GetById(NotaId);

                if (nota == null)
                    return;

                nota.Status = Status;

                _notaService.Update(NotaId, nota);
            }
        }
    }
}
