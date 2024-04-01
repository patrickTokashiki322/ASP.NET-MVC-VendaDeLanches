using ASP.NET_MVC_VendaDeLanches.Models;
using ASP.NET_MVC_VendaDeLanches.Repositories.Interfaces;
using ASP.NET_MVC_VendaDeLanches.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_MVC_VendaDeLanches.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheRepository _lancheRepository;

        public LancheController(ILancheRepository lancheRepository)
        {
            _lancheRepository = lancheRepository;
        }

        public IActionResult List(string categoria)
        {
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(categoria))
            {
                lanches = _lancheRepository.Lanches.OrderBy(l => l.LancheId);
                categoriaAtual = "Todos os lanches";
            }
            else
            {
                lanches = _lancheRepository.Lanches.Where(c => c.Categoria.CategoriaNome.Equals(categoria));

                categoriaAtual = categoria;
            }

            var lanchesListViewModel = new LancheListViewModel
            {
                lanches = lanches,
                CategoriaAtual = categoriaAtual
            };

            return View(lanchesListViewModel);
        }

        public IActionResult Details(int lancheId)
        {
            var lanche = _lancheRepository.Lanches.FirstOrDefault(l => l.LancheId == lancheId);

            return View(lanche);
        }
    }
}
