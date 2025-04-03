using AutoMapper;
using NotaFiscalFaturamento.Application.DTOs;
using NotaFiscalFaturamento.Application.Interfaces;
using NotaFiscalFaturamento.Domain.Entities;
using NotaFiscalFaturamento.Domain.Interfaces;

namespace NotaFiscalFaturamento.Application.Services
{
    public class NotaService(INotaRepository notaRepository, IMapper mapper) : INotaService
    {
        private readonly INotaRepository _notaRepository = notaRepository;
        private readonly IMapper _mapper = mapper;


        public NotaDTO GetById(int id)
        {
            var notaEntity = _notaRepository.GetById(id);

            return _mapper.Map<NotaDTO>(notaEntity);
        }

        public IEnumerable<NotaDTO> GetNotas()
        {
            var notasEntity = _notaRepository.GetNotas();

            return _mapper.Map<IEnumerable<NotaDTO>>(notasEntity);
        }

        public NotaDTO? Create(NotaDTO notaDTO)
        {
            var notaEntity = _mapper.Map<Nota>(notaDTO);

            return _mapper.Map<NotaDTO>(_notaRepository.Create(notaEntity));
        }

        
        public NotaDTO? Update(int id, NotaDTO notaDTO)
        {
            var notaEntity = _mapper.Map<Nota>(notaDTO);

            return _mapper.Map<NotaDTO>(_notaRepository.Update(id, notaEntity));
        }

        public bool Remove(int id)
        {
            return _notaRepository.Remove(id);
        }
    }
}
