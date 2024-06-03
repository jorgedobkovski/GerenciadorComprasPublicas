
using System.ComponentModel.DataAnnotations;

namespace GerenciadorComprasPublicas.Models
{
    public class HistoricoPreco
    {
        [Key]
        public int HistoricoId { get; set; }
        [Required]
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }
        [Required]
        public DateTime Data { get; set; }
        [Required]
        public decimal Preco { get; set; }
    }
}
