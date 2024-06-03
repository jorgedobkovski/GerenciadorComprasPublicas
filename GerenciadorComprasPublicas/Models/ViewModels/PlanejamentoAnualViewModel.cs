using System.ComponentModel.DataAnnotations;

namespace GerenciadorComprasPublicas.Models.ViewModels
{
    public class PlanejamentoAnualViewModel
    {
        public int Id { get; set; }
        public int SecretariaId { get; set; }
        public int Ano { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Descricao { get; set; }
        public virtual ICollection<PlanejamentoItemViewModel> ItensPlanejados { get; set; }
        public string? SecretariaNome { get; internal set; }
    }
}
