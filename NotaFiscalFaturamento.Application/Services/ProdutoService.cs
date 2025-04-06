using AutoMapper;
using NotaFiscalFaturamento.Application.DTOs;
using NotaFiscalFaturamento.Application.Interfaces;
using NotaFiscalFaturamento.Domain.Entities;
using NotaFiscalFaturamento.Domain.Interfaces;

namespace NotaFiscalFaturamento.Application.Services
{
    public class ProdutoService(IProdutoRepository produtoRepository, IMapper mapper) : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository = produtoRepository;
        private readonly IMapper _mapper = mapper;


        public ProdutoDTO GetById(int id)
        {
            var produtoEntity = _produtoRepository.GetById(id);

            return _mapper.Map<ProdutoDTO>(produtoEntity);
        }

        public IEnumerable<ProdutoDTO> GetProdutos()
        {
            var produtosEntity = _produtoRepository.GetProdutos();

            return _mapper.Map<IEnumerable<ProdutoDTO>>(produtosEntity);
        }

        public ProdutoDTO? Create(ProdutoDTO produtoDTO)
        {
            var produtoEntity = _mapper.Map<Produto>(produtoDTO);

            return _mapper.Map<ProdutoDTO>(_produtoRepository.Create(produtoEntity));
        }

        
        public ProdutoDTO? Update(int id, ProdutoDTO produtoDTO)
        {
            var produtoEntity = _mapper.Map<Produto>(produtoDTO);

            return _mapper.Map<ProdutoDTO>(_produtoRepository.Update(id, produtoEntity));
        }

        public bool Remove(int id)
        {
            return _produtoRepository.Remove(id);
        }
    }
}
