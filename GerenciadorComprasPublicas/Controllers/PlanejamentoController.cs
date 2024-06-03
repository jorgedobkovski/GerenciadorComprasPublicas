using GerenciadorComprasPublicas.Data;
using GerenciadorComprasPublicas.Models.ViewModels;
using GerenciadorComprasPublicas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorComprasPublicas.Controllers
{
    public class PlanejamentoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlanejamentoController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var planejamentos = _context.PlanejamentosAnuais
                .Include(p => p.ItensPlanejados)
                .Include(P =>  P.Secretaria)
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

        //public async Task<IActionResult> Index(int secretariaId)
        //{
        //    var planejamentos = await _context.PlanejamentosAnuais
        //                                       .Where(p => p.SecretariaId == secretariaId)
        //                                       .Include(p => p.ItensPlanejados)
        //                                       .ToListAsync();
        //    return View(planejamentos);
        //}

        public ActionResult Create()
        {
            ViewBag.SecretariaId = new SelectList(_context.Secretarias, "SecretariaId", "Nome");
            ViewBag.ItemId = new SelectList(_context.Itens, "ItemId", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlanejamentoAnualViewModel planejamentoAnual)
        {
            if (ModelState.IsValid)
            {
                var planejamento = new PlanejamentoAnual
                {
                    SecretariaId = planejamentoAnual.SecretariaId,
                    Ano = planejamentoAnual.Ano,
                    DataCriacao = planejamentoAnual.DataCriacao,
                    Descricao = planejamentoAnual.Descricao,
                    ItensPlanejados = planejamentoAnual.ItensPlanejados.Select(i => new PlanejamentoItem
                    {
                        ItemId = i.ItemId,
                        QuantidadePlanejada = i.QuantidadePlanejada,
                        ValorEstimado = i.ValorEstimado,
                        ValorGasto = i.ValorGasto,
                        DataUltimaAtualizacao = i.DataUltimaAtualizacao
                    }).ToList()
                };

                _context.PlanejamentosAnuais.Add(planejamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.SecretariaId = new SelectList(_context.Secretarias, "SecretariaId", "Nome", planejamentoAnual.SecretariaId);
            ViewBag.ItemId = new SelectList(_context.Itens, "ItemId", "Nome");
            return View(planejamentoAnual);
        }

        public async Task<IActionResult> Details(int id)
        {
            var planejamento = await _context.PlanejamentosAnuais
                                              .Include(p => p.ItensPlanejados)
                                              .ThenInclude(pi => pi.Item)
                                              .FirstOrDefaultAsync(p => p.Id == id);

            if (planejamento == null)
            {
                return NotFound();
            }

           

            var viewModel = new PlanejamentoAnualViewModel
            {
                Id = planejamento.Id,
                SecretariaId = planejamento.SecretariaId,
                Ano = planejamento.Ano,
                DataCriacao = planejamento.DataCriacao,
                Descricao = planejamento.Descricao,
                ItensPlanejados = planejamento.ItensPlanejados.Select(pi => new PlanejamentoItemViewModel
                {
                    Id = pi.Id,
                    ItemId = pi.ItemId,
                    ItemNome = pi.Item.Nome,
                    QuantidadePlanejada = pi.QuantidadePlanejada,
                    ValorEstimado = pi.ValorEstimado,
                    ValorGasto = pi.ValorGasto,
                    DataUltimaAtualizacao = pi.DataUltimaAtualizacao
                }).ToList()
            };

            return View(viewModel);
        }

        public async Task<IActionResult> ItemDetails(int id)
        {
            var item = await _context.PlanejamentosItens
                                        .Include(i => i.Item)
                                        .Include(i => i.PlanejamentoAnual)
                                        .FirstOrDefaultAsync(i => i.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            // Calcular média de consumo de itens pela secretaria
            var mediaConsumo = await CalcularMediaConsumoAsync(item.PlanejamentoAnual.SecretariaId, item.ItemId);

            // Calcular tempo médio do processo licitatório para o item específico
            var tempoMedioProcessoLicitatorio = await CalcularTempoMedioProcessoLicitatorioAsync(item.ItemId);

            // Recuperar a última licitação para o item específico
            var ultimaLicitacao = await RecuperaUltimaLicitacaoAsync(item.PlanejamentoAnual.SecretariaId, item.ItemId);

            // Data final da última licitação
            var dataFinalUltimaLicitacao = ultimaLicitacao.DataFim;

            // Estimar o número de dias que o estoque atual durará
            var diasEstoqueDuracao = ultimaLicitacao.QuantidadeItem / mediaConsumo;

            // Estimar a data em que o estoque será esgotado
            var dataEstoqueEsgotado = dataFinalUltimaLicitacao.AddDays(diasEstoqueDuracao);

            // Calcular a data para iniciar o novo processo licitatório
            var dataInicioNovoProcesso = dataEstoqueEsgotado.AddDays(-tempoMedioProcessoLicitatorio - 60);


            var viewModel = new PlanejamentoItemViewModel
            {
                ItemId = item.ItemId,
                ItemNome = item.Item.Nome,
                Descricao = item.Item.Descricao,
                Categoria = item.Item.Categoria,
                UnidadeMedida = item.Item.UnidadeMedida
            };

            // Armazenar as informações na ViewBag
            ViewBag.MediaConsumo = mediaConsumo;
            ViewBag.TempoMedioProcessoLicitatorio = tempoMedioProcessoLicitatorio;
            ViewBag.DataFinalUltimaLicitacao = dataFinalUltimaLicitacao;
            ViewBag.DiasEstoqueDuracao = diasEstoqueDuracao;
            ViewBag.DataEstoqueEsgotado = dataEstoqueEsgotado;
            ViewBag.DataInicioNovoProcesso = dataInicioNovoProcesso;

            return View(viewModel);
        }

        private async Task<double> CalcularMediaConsumoAsync(int secretariaId, int itemId)
        {
            var licitacoes = await _context.Licitacoes
                .Where(l => l.SecretariaId == secretariaId && l.ItemId == itemId)
                .OrderByDescending(l => l.DataInicio)
                .Take(3)
                .ToListAsync();

            if (licitacoes.Count < 2)
            {
                // Se houver menos de duas licitações, não é possível calcular o consumo por dia
                return 0.0;
            }

            double somaDiferencasDias = 0.0;
            double quantidadeTotalLicitada = 0.0;

            for (int i = 0; i < licitacoes.Count - 1; i++)
            {
                var diferencaDias = (licitacoes[i].DataInicio - licitacoes[i + 1].DataInicio).TotalDays;
                somaDiferencasDias += diferencaDias;
            }

            quantidadeTotalLicitada = licitacoes.Sum(l => l.QuantidadeItem);

            // Calcular a média de consumo (quantidade total licitada dividido pelo número total de dias)
            var consumoMedioSecretaria = quantidadeTotalLicitada / somaDiferencasDias;

            return consumoMedioSecretaria;
        }


        private async Task<double> CalcularTempoMedioProcessoLicitatorioAsync(int itemId)
        {
            var licitacoes = await _context.Licitacoes
                .Where(l => l.ItemId == itemId)
                .OrderByDescending(l => l.DataInicio)
                .Take(3)
                .ToListAsync();

            if (licitacoes.Count == 0)
            {
                // Se não houver licitações, retorne 0
                return 0.0;
            }

            double somaTempoProcessos = 0;

            // Calcular a soma dos tempos dos processos licitatórios
            foreach (var licitacao in licitacoes)
            {
                var tempoProcesso = (licitacao.DataFim - licitacao.DataInicio).TotalDays;
                somaTempoProcessos += tempoProcesso;
            }

            // Calcular a média dos tempos dos processos licitatórios
            double tempoMedioProcessoLicitatorio = somaTempoProcessos / licitacoes.Count;

            return tempoMedioProcessoLicitatorio;
        }

        private async Task<Licitacao?> RecuperaUltimaLicitacaoAsync(int secretariaId, int itemId)
        {
            // Recuperar a última licitação para o item específico
            var ultimaLicitacao = await _context.Licitacoes
                .Where(l => l.SecretariaId == secretariaId && l.ItemId == itemId)
                .OrderByDescending(l => l.DataFim)
                .FirstOrDefaultAsync();

            return ultimaLicitacao;
        }
        
    }
}
