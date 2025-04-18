﻿using NotaFiscalFaturamento.Domain.Enums;
using System.Text.Json.Serialization;

namespace NotaFiscalFaturamento.Application.DTOs
{
    public class NotaDTO
    {
        public int? Id { get; set; }
        
        public int Status { get; set; }

        public List<ProdutoDTO> Produtos { get; set; } = [];
    }
}
