using GerenciadorComprasPublicas.Data;
using GerenciadorComprasPublicas.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorComprasPublicas.Controllers
{
    public class LicitacaoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LicitacaoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Licitacao/Create
        public IActionResult Create()
        {
            var viewModel = new LicitacaoViewModel();
            viewModel.FasesLicitacao = new List<FaseLicitacaoViewModel>();
            return View(viewModel);
        }

        // POST: /Licitacao/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LicitacaoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Mapear viewModel para Licitacao e salvar no banco de dados
                // ...
                return RedirectToAction("Index", "Home");
            }
            return View(viewModel);
        }
    }

}
