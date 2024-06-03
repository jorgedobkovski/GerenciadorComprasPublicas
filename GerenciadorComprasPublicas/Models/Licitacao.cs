using System.ComponentModel.DataAnnotations;

namespace GerenciadorComprasPublicas.Models
{
    public class Licitacao
    {
        [Key]
        public int LicitacaoId { get; set; }
        [Required]
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }
        public int QuantidadeItem { get; set; }
        [Required]
        public decimal ValorEstimadoItem { get; set; }
        public decimal ValorHomologadoItem { get; set; }
        public decimal ValorEstimadoTotal { get; set; }
        public decimal ValorHomologadoTotal { get; set; }
        public int FornecedorId { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
        [Required]
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        [MaxLength(50)]
        public string Status { get; set; }
        [MaxLength(500)]
        public string? MotivoFracasso { get; set; }

        public virtual ICollection<FaseLicitacao> FasesLicitacao { get; set; }
        public int SecretariaId { get; set; }
        public virtual Secretaria Secretaria { get; set; }
    }
}
