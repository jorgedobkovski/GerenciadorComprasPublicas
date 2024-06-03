using System.ComponentModel.DataAnnotations;

namespace GerenciadorComprasPublicas.Models
{
    public class Fornecedor
    {
        [Key]
        public int FornecedorId { get; set; }
        [Required]
        [MaxLength(200)]
        public string Nome { get; set; }
        [MaxLength(500)]
        public string Endereco { get; set; }
        [MaxLength(15)]
        public string Telefone { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        public double HistoricoPontualidade { get; set; }
        public double QualidadeMedia { get; set; }
        public virtual ICollection<Licitacao> Licitacoes { get; set; }
    }
}
