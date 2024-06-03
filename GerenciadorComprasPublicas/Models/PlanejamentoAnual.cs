using System.ComponentModel.DataAnnotations;

namespace GerenciadorComprasPublicas.Models
{
    public class PlanejamentoAnual
    {
        [Key]
        public int Id { get; set; }
        public int SecretariaId { get; set; }
        public virtual Secretaria Secretaria { get; set; }  // Associação com a classe Secretaria
        public int Ano { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Descricao { get; set; }
        public virtual ICollection<PlanejamentoItem> ItensPlanejados { get; set; }
    }
}
