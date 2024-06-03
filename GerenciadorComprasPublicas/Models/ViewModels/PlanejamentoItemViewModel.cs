namespace GerenciadorComprasPublicas.Models.ViewModels
{
    public class PlanejamentoItemViewModel
    {
        public int Id { get; set; }
        public int PlanejamentoId { get; set; }
        public int ItemId { get; set; }
        public int QuantidadePlanejada { get; set; }
        public decimal ValorEstimado { get; set; }
        public decimal ValorGasto { get; set; }
        public DateTime DataUltimaAtualizacao { get; set; }
        public string? ItemNome { get; internal set; }
        public string? Descricao { get; internal set; }
        public string? Categoria { get; internal set; }
        public string? UnidadeMedida { get; internal set; }
    }
}
