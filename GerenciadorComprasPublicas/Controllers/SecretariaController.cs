using GerenciadorComprasPublicas.Data;
using GerenciadorComprasPublicas.Models.ViewModels;
using GerenciadorComprasPublicas.Models;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorComprasPublicas.Controllers
{
    public class SecretariaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SecretariaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Secretaria/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Secretaria/Create
        [HttpPost]
        public ActionResult Create(SecretariaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var secretaria = new Secretaria
                {
                    Nome = model.Nome,
                    Descricao = model.Descricao,
                    Telefone = model.Telefone,
                    Email = model.Email,
                    Endereco = model.Endereco
                };

                _context.Secretarias.Add(secretaria);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}
