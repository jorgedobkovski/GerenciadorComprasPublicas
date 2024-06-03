using System.ComponentModel.DataAnnotations;

namespace GerenciadorComprasPublicas.Models
{
    public class Orcamento
    {
        [Key]
        public int OrcamentoId { get; set; }
        [Required]
        public int SecretariaId { get; set; }
        public virtual Secretaria Secretaria { get; set; }
        [Required]
        public int Ano { get; set; }
        [Required]
        public decimal ValorPlanejado { get; set; }
        public decimal ValorGasto { get; set; }
        public decimal SaldoDisponivel { get; set; }
    }
}
