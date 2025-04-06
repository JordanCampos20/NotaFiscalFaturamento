using NotaFiscalFaturamento.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotaFiscalFaturamento.Domain.Entities
{
    public class Nota
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public StatusEnum Status { get; set; } = StatusEnum.Aberta;

        public ICollection<Produto> Produtos { get; set; } = [];

        public DateTime DataCadastro { get; set; } = DateTime.UtcNow.AddHours(-3);
    }
}
