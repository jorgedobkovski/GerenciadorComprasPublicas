using GerenciadorComprasPublicas.Data;
using GerenciadorComprasPublicas.Models;
using GerenciadorComprasPublicas.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorComprasPublicas.Controllers
{
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Item/Create
        public IActionResult Create()
        {
            var viewModel = new ItemViewModel();
            return View(viewModel);
        }

        // POST: /Item/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                var item = new Item
                {
                    Nome = model.Nome,
                    Descricao = model.Descricao,
                    Categoria = model.Categoria,
                    UnidadeMedida = model.UnidadeMedida
                };

                _context.Add(item);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }

}
