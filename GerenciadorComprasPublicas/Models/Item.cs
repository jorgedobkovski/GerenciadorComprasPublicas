using System.ComponentModel.DataAnnotations;

namespace GerenciadorComprasPublicas.Models
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }
        [MaxLength(500)]
        public string Descricao { get; set; }
        [MaxLength(100)]
        public string Categoria { get; set; }
        [MaxLength(50)]
        public string UnidadeMedida { get; set; }

        public virtual ICollection<Estoque> Estoques { get; set; }
        public virtual ICollection<Licitacao> Licitacoes { get; set; }
        public virtual ICollection<HistoricoPreco> HistoricosPreco { get; set; }
        public virtual ICollection<PlanejamentoItem> Planejamentos { get; set; }
    }
}
