using System.ComponentModel.DataAnnotations;

namespace GerenciadorComprasPublicas.Models
{
    public class Secretaria
    {
        [Key]
        public int SecretariaId { get; set; }
        [Required]
        [MaxLength(200)]
        public string Nome { get; set; }
        [MaxLength(500)]
        public string Descricao { get; set; }
        [MaxLength(500)]
        public string Endereco { get; set; }
        [MaxLength(15)]
        public string Telefone { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }

        public virtual ICollection<Orcamento> Orcamentos { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
        public virtual ICollection<PlanejamentoAnual> PlanejamentosAnuais { get; set; }
    }
}
