using System.ComponentModel.DataAnnotations;

namespace GerenciadorComprasPublicas.Models
{
    public class Estoque
    {
        [Key]
        public int EstoqueId { get; set; }
        [Required]
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }
        [Required]
        public int SecretariaId { get; set; }
        public virtual Secretaria Secretaria { get; set; }
        [Required]
        public int QuantidadeAtual { get; set; }
        public int QuantidadeMinima { get; set; }
        public int QuantidadeMaxima { get; set; }
        [Required]
        public DateTime DataAtualizacao { get; set; }
    }
}
