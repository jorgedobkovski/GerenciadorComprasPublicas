using System.ComponentModel.DataAnnotations;

namespace GerenciadorComprasPublicas.Models.ViewModels
{
    public class ItemViewModel
    {
        public int ItemId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public string UnidadeMedida { get; set; }
    }
}
