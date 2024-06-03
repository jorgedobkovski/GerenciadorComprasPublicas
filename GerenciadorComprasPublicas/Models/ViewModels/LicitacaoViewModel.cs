namespace GerenciadorComprasPublicas.Models.ViewModels
{
    public class LicitacaoViewModel
    {
        public int ItemId { get; set; }
        public int FornecedorId { get; set; }
        public decimal ValorEstimado { get; set; }
        public decimal ValorHomologado { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string Status { get; set; }
        public string MotivoFracasso { get; set; }
        public List<FaseLicitacaoViewModel> FasesLicitacao { get; set; }
    }

    public class FaseLicitacaoViewModel
    {
        public string NomeFase { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
    }

}
