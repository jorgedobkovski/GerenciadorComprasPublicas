using System.ComponentModel.DataAnnotations;

namespace GerenciadorComprasPublicas.Models
{
    public class FaseLicitacao
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int LicitacaoId { get; set; }
        public virtual Licitacao Licitacao { get; set; }
        [Required]
        [MaxLength(100)]
        public string NomeFase { get; set; }
        [Required]
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public int Duracao { get; set; }
    }
}
