using GerenciadorComprasPublicas.Data;
using GerenciadorComprasPublicas.Models;
using GerenciadorComprasPublicas.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GerenciadorComprasPublicas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var planejamentos = _context.PlanejamentosAnuais
                .Include(p => p.ItensPlanejados)
                .Include(P => P.Secretaria)
                .ToList();
            var viewModel = planejamentos.Select(p => new PlanejamentoAnualViewModel
            {
                Id = p.Id,
                SecretariaId = p.SecretariaId,
                SecretariaNome = p.Secretaria.Nome,
                Ano = p.Ano,
                DataCriacao = p.DataCriacao,
                Descricao = p.Descricao,
                ItensPlanejados = p.ItensPlanejados.Select(pi => new PlanejamentoItemViewModel
                {
                    Id = pi.Id,
                    ItemId = pi.ItemId,
                    QuantidadePlanejada = pi.QuantidadePlanejada,
                    ValorEstimado = pi.ValorEstimado,
                    ValorGasto = pi.ValorGasto,
                    DataUltimaAtualizacao = pi.DataUltimaAtualizacao
                }).ToList()
            }).ToList();

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult PopularBD()
        {
            var items = new List<Item>
            {
                new Item { Nome = "Café em pó", Descricao = "Café especial moído de 250 gramas é um café especial com aroma da mais perfeita combinação entre chocolate e avelã. Perfeito para seus momentos mais especiais.", Categoria = "Alimentício", UnidadeMedida = "Pacotes" },
                new Item { Nome = "Blocos de anoteções", Descricao = " 2 vias - numeradas. Formato: 10x15 cm. Papel Sulfite 56grs (1ª via) e Jornal 48 grs (2ª via).", Categoria = "Papelaria", UnidadeMedida = "Unidade" },
                new Item { Nome = "Álcool Gel 70%", Descricao = "Gel antisséptico extrato de algodão sem perfume com hidratante 500ml, produto não testado em animais, muito mais que álcool gel.", Categoria = "Higiene", UnidadeMedida = "Mililitro" },
                new Item { Nome = "Porta Caneta Organizador", Descricao = "O porta treco multiuso é ideal para manter a sua mesa de escritório ou da mesa de estudos organizada e limpa. ", Categoria = "Utensílio", UnidadeMedida = "Unidade" },
                new Item { Nome = "Lixeira de Metal Redonda", Descricao = "A Lixeira de metal redonda de 8,5L , sofisticada e ideal para ser utilizada em escritórios ou home office.", Categoria = "Utensílio", UnidadeMedida = "Unidade" }
            };
            items.ForEach(i => _context.Itens.Add(i));

            _context.SaveChanges();

            var historicoPrecos = new List<HistoricoPreco>
            {
                new HistoricoPreco { ItemId = items[0].ItemId, Preco = 24.75m, Data = DateTime.Now.AddMonths(-3) },
                new HistoricoPreco { ItemId = items[0].ItemId, Preco = 25.05m, Data = DateTime.Now.AddMonths(-2) },
                new HistoricoPreco { ItemId = items[0].ItemId, Preco = 19.90m, Data = DateTime.Now.AddMonths(-1) },
                new HistoricoPreco { ItemId = items[1].ItemId, Preco = 14.0m, Data = DateTime.Now.AddMonths(-3) },
                new HistoricoPreco { ItemId = items[1].ItemId, Preco = 14.0m, Data = DateTime.Now.AddMonths(-2) },
                new HistoricoPreco { ItemId = items[1].ItemId, Preco = 14.0m, Data = DateTime.Now.AddMonths(-1) },
                new HistoricoPreco { ItemId = items[2].ItemId, Preco = 7.5m, Data = DateTime.Now.AddMonths(-3) },
                new HistoricoPreco { ItemId = items[2].ItemId, Preco = 7.0m, Data = DateTime.Now.AddMonths(-2) },
                new HistoricoPreco { ItemId = items[2].ItemId, Preco = 7.0m, Data = DateTime.Now.AddMonths(-1) }
            };
            historicoPrecos.ForEach(h => _context.HistoricoPrecos.Add(h));

            _context.SaveChanges();

            var secretarias = new List<Secretaria>
            {
                new Secretaria { Nome = "SEUM", Descricao = "Secretaria Número Um do Estado.", Endereco = "Rua João Silva", Telefone = "123456789", Email = "ium@example.com" },
                new Secretaria { Nome = "SEDOIS", Descricao = "Secretaria Número Dois do Estado.", Endereco = "Rua João Silva", Telefone = "123456789", Email = "educacao@example.com" },
                new Secretaria { Nome = "SETRÊS", Descricao = "Secretaria Número Tres do Estado.", Endereco = "Av. Maria Oliveira", Telefone = "987654321", Email = "saude@example.com" },
                new Secretaria { Nome = "SEQUATRO", Descricao = "Secretaria Número Quatro do Estado.", Endereco = "Rua Pedro Souza", Telefone = "456789123", Email = "transporte@example.com" },
                new Secretaria { Nome = "SECINCO", Descricao = "Secretaria Número Cinco do Estado.", Endereco = "R. Ana Costa", Telefone = "321654987", Email = "seguranca@example.com" },
                new Secretaria { Nome = "SESSEIS", Descricao = "Secretaria Número Seis do Estado.", Endereco = "Rua Carlos Lima", Telefone = "654987321", Email = "cultura@example.com" }
            };
            secretarias.ForEach(s => _context.Secretarias.Add(s));

            _context.SaveChanges();

            var estoques = new List<Estoque>
            {
                new Estoque { ItemId = items[0].ItemId, SecretariaId = secretarias[0].SecretariaId, QuantidadeAtual = 100, QuantidadeMinima = 20, QuantidadeMaxima = 200, DataAtualizacao = DateTime.Now },
                new Estoque { ItemId = items[1].ItemId, SecretariaId = secretarias[1].SecretariaId, QuantidadeAtual = 150, QuantidadeMinima = 30, QuantidadeMaxima = 250, DataAtualizacao = DateTime.Now },
                new Estoque { ItemId = items[2].ItemId, SecretariaId = secretarias[2].SecretariaId, QuantidadeAtual = 200, QuantidadeMinima = 40, QuantidadeMaxima = 300, DataAtualizacao = DateTime.Now },
                new Estoque { ItemId = items[3].ItemId, SecretariaId = secretarias[3].SecretariaId, QuantidadeAtual = 80, QuantidadeMinima = 10, QuantidadeMaxima = 150, DataAtualizacao = DateTime.Now }                
            };
            estoques.ForEach(e => _context.Estoques.Add(e));

            _context.SaveChanges();

            var fornecedores = new List<Fornecedor>
            {
                new Fornecedor { Nome = "Fornecedor A", Endereco = "Endereço do Fornecedor A", Telefone = "123456789", Email = "fornecedorA@example.com", HistoricoPontualidade = 4.5, QualidadeMedia = 3.8 },
                new Fornecedor { Nome = "Fornecedor B", Endereco = "Endereço do Fornecedor B", Telefone = "987654321", Email = "fornecedorB@example.com", HistoricoPontualidade = 4.2, QualidadeMedia = 4.0 },
                new Fornecedor { Nome = "Fornecedor C", Endereco = "Endereço do Fornecedor C", Telefone = "456789123", Email = "fornecedorC@example.com", HistoricoPontualidade = 3.9, QualidadeMedia = 4.2 }
            };
            fornecedores.ForEach(f => _context.Fornecedores.Add(f));


            _context.SaveChanges();

            var licitacoes = new List<Licitacao>
            {
                new Licitacao { SecretariaId = secretarias[0].SecretariaId, ItemId = items[0].ItemId, NumeroProcesso = "0043.002064/2020-43", NumeroCertame="00651/2021", FornecedorId = fornecedores[0].FornecedorId, QuantidadeItem = 300, ValorEstimadoItem = 2900.00m, ValorHomologadoItem = 2833.33m, ValorEstimadoTotal = 870000.00m, ValorHomologadoTotal = 850000.00m, DataInicio = new DateTime(2020, 1, 15), DataFim = new DateTime(2020, 3, 15), Status = "Concluído" },
                new Licitacao { SecretariaId = secretarias[0].SecretariaId, ItemId = items[0].ItemId, NumeroProcesso = "0067.002567/2022-98", NumeroCertame="00542/2022",FornecedorId = fornecedores[0].FornecedorId, QuantidadeItem = 250, ValorEstimadoItem = 2920.00m, ValorHomologadoItem = 2880.00m, ValorEstimadoTotal = 730000.00m, ValorHomologadoTotal = 720000.00m, DataInicio = new DateTime(2022, 1, 18), DataFim = new DateTime(2021, 3, 28), Status = "Concluído" },
                new Licitacao { SecretariaId = secretarias[0].SecretariaId, ItemId = items[0].ItemId, NumeroProcesso = "0044.000100/2023-49", NumeroCertame="00011/2023",FornecedorId = fornecedores[0].FornecedorId, QuantidadeItem = 350, ValorEstimadoItem = 2171.43m, ValorHomologadoItem = 2114.28m, ValorEstimadoTotal = 760000.00m, ValorHomologadoTotal = 740000.00m, DataInicio = new DateTime(2024, 2, 1), DataFim = new DateTime(2024, 4, 29), Status = "Concluído" },
                new Licitacao { SecretariaId = secretarias[0].SecretariaId, ItemId = items[1].ItemId, NumeroProcesso = "0023.006454/2024-43", NumeroCertame="00651/2024", FornecedorId = fornecedores[1].FornecedorId, QuantidadeItem = 130, ValorEstimadoItem = 15.00m, ValorHomologadoItem = 14.00m, ValorEstimadoTotal = 130*15.00m, ValorHomologadoTotal = 130*14.00m, DataInicio = new DateTime(2024, 4, 19), DataFim = new DateTime(2024, 6, 20), Status = "Concluído" },
                new Licitacao { SecretariaId = secretarias[0].SecretariaId, ItemId = items[1].ItemId, NumeroProcesso = "0065.002066/2024-45", NumeroCertame="00345/2024", FornecedorId = fornecedores[1].FornecedorId, QuantidadeItem = 150, ValorEstimadoItem = 15.00m, ValorHomologadoItem = 14.00m, ValorEstimadoTotal = 150*15.00m, ValorHomologadoTotal = 150*14.00m, DataInicio = new DateTime(2024, 4, 19), DataFim = new DateTime(2024, 6, 20), Status = "Concluído" },
                new Licitacao { SecretariaId = secretarias[0].SecretariaId, ItemId = items[1].ItemId, NumeroProcesso = "0043.002111/2024-24", NumeroCertame="00456/2024", FornecedorId = fornecedores[1].FornecedorId, QuantidadeItem = 150, ValorEstimadoItem = 15.00m, ValorHomologadoItem = 14.00m, ValorEstimadoTotal = 150*15.00m, ValorHomologadoTotal = 150*14.00m, DataInicio = new DateTime(2024, 4, 19), DataFim = new DateTime(2024, 6, 20), Status = "Concluído" },
                new Licitacao { SecretariaId = secretarias[0].SecretariaId, ItemId = items[2].ItemId, NumeroProcesso = "0012.002444/2024-44", NumeroCertame="00745/2024", FornecedorId = fornecedores[2].FornecedorId, QuantidadeItem = 90, ValorEstimadoItem = 7.00m, ValorHomologadoItem = 5.00m, ValorEstimadoTotal = 90*7.00m, ValorHomologadoTotal = 90*5.00m, DataInicio = new DateTime(2024, 4, 9), DataFim = new DateTime(2024, 6, 11), Status = "Em Andamento" },
            };
            licitacoes.ForEach(l => _context.Licitacoes.Add(l));


            _context.SaveChanges();

            var fasesLicitacao = new List<FaseLicitacao>
            {
                new FaseLicitacao { LicitacaoId = licitacoes[0].LicitacaoId, NomeFase = "Preparação", DataInicio = DateTime.Now.AddDays(-35), DataFim = DateTime.Now.AddDays(-30) },
                new FaseLicitacao { LicitacaoId = licitacoes[0].LicitacaoId, NomeFase = "Recebimento de Propostas", DataInicio = DateTime.Now.AddDays(-25), DataFim = DateTime.Now.AddDays(-20) },
                new FaseLicitacao { LicitacaoId = licitacoes[0].LicitacaoId, NomeFase = "Avaliação de Propostas", DataInicio = DateTime.Now.AddDays(-15), DataFim = DateTime.Now.AddDays(-10) },
                new FaseLicitacao { LicitacaoId = licitacoes[0].LicitacaoId, NomeFase = "Adjudicação", DataInicio = DateTime.Now.AddDays(-5), DataFim = DateTime.Now },
                new FaseLicitacao { LicitacaoId = licitacoes[1].LicitacaoId, NomeFase = "Preparação", DataInicio = DateTime.Now.AddDays(-45), DataFim = DateTime.Now.AddDays(-40) },
                new FaseLicitacao { LicitacaoId = licitacoes[1].LicitacaoId, NomeFase = "Recebimento de Propostas", DataInicio = DateTime.Now.AddDays(-35), DataFim = DateTime.Now.AddDays(-30) },
                new FaseLicitacao { LicitacaoId = licitacoes[1].LicitacaoId, NomeFase = "Avaliação de Propostas", DataInicio = DateTime.Now.AddDays(-25), DataFim = DateTime.Now.AddDays(-20) },
                new FaseLicitacao { LicitacaoId = licitacoes[1].LicitacaoId, NomeFase = "Adjudicação", DataInicio = DateTime.Now.AddDays(-15), DataFim = DateTime.Now.AddDays(-10) },
                new FaseLicitacao { LicitacaoId = licitacoes[2].LicitacaoId, NomeFase = "Preparação", DataInicio = DateTime.Now.AddDays(-55), DataFim = DateTime.Now.AddDays(-50) },
                new FaseLicitacao { LicitacaoId = licitacoes[2].LicitacaoId, NomeFase = "Recebimento de Propostas", DataInicio = DateTime.Now.AddDays(-45), DataFim = DateTime.Now.AddDays(-40) },
                new FaseLicitacao { LicitacaoId = licitacoes[2].LicitacaoId, NomeFase = "Avaliação de Propostas", DataInicio = DateTime.Now.AddDays(-35), DataFim = DateTime.Now.AddDays(-30) },
                new FaseLicitacao { LicitacaoId = licitacoes[2].LicitacaoId, NomeFase = "Adjudicação", DataInicio = DateTime.Now.AddDays(-25), DataFim = DateTime.Now.AddDays(-20) },
            };
            fasesLicitacao.ForEach(fl => _context.FasesLicitacao.Add(fl));


            _context.SaveChanges();
            var orcamentos = new List<Orcamento>
            {
                new Orcamento { SecretariaId = secretarias[0].SecretariaId, Ano = 2024, ValorPlanejado = 100000.00m, ValorGasto = 50000.00m, SaldoDisponivel = 50000.00m },
                new Orcamento { SecretariaId = secretarias[1].SecretariaId, Ano = 2024, ValorPlanejado = 150000.00m, ValorGasto = 75000.00m, SaldoDisponivel = 75000.00m },
                new Orcamento { SecretariaId = secretarias[2].SecretariaId, Ano = 2024, ValorPlanejado = 200000.00m, ValorGasto = 100000.00m, SaldoDisponivel = 100000.00m },
                new Orcamento { SecretariaId = secretarias[3].SecretariaId, Ano = 2024, ValorPlanejado = 120000.00m, ValorGasto = 60000.00m, SaldoDisponivel = 60000.00m },
                new Orcamento { SecretariaId = secretarias[4].SecretariaId, Ano = 2024, ValorPlanejado = 160000.00m, ValorGasto = 80000.00m, SaldoDisponivel = 80000.00m }
            };
            orcamentos.ForEach(o => _context.Orcamentos.Add(o));

            _context.SaveChanges();
            return Ok();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}