using System.ComponentModel.DataAnnotations;

namespace GerenciadorComprasPublicas.Models
{
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        [Required]
        [MaxLength(100)]
        public string Senha { get; set; }
        [Required]
        [MaxLength(50)]
        public string TipoUsuario { get; set; }
        [Required]
        public int SecretariaId { get; set; }
        public virtual Secretaria Secretaria { get; set; }
    }
}
