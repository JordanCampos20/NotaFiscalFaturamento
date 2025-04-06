using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotaFiscalFaturamento.Domain.Entities
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int NotaId { get; set; }

        [Required]
        public int ProdutoId { get; set; }

        [Required]
        public int Quantidade { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.UtcNow.AddHours(-3);

        [ForeignKey("NotaId")]
        public Nota? Nota { get; set; }
    }
}
