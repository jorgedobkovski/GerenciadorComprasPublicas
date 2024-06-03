using System.ComponentModel.DataAnnotations;

namespace GerenciadorComprasPublicas.Models
{
    public class PlanejamentoItem
    {
        [Key]
        public int Id { get; set; }
        public int PlanejamentoAnualId { get; set; }
        public virtual PlanejamentoAnual PlanejamentoAnual { get; set; } 
        public int ItemId { get; set; }
        public virtual Item Item { get; set; } 
        public int QuantidadePlanejada { get; set; }
        public decimal ValorEstimado { get; set; }
        public decimal ValorGasto { get; set; }
        public DateTime DataUltimaAtualizacao { get; set; }
    }
}
