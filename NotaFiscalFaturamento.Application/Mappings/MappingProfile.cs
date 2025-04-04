using AutoMapper;
using NotaFiscalFaturamento.Application.DTOs;
using NotaFiscalFaturamento.Domain.Entities;

namespace NotaFiscalFaturamento.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Nota, NotaDTO>().ReverseMap();
            CreateMap<Produto, ProdutoDTO>().ReverseMap();
        }
    }
}
