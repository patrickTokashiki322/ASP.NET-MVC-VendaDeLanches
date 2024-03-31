using ASP.NET_MVC_VendaDeLanches.Context;
using ASP.NET_MVC_VendaDeLanches.Models;
using ASP.NET_MVC_VendaDeLanches.Repositories.Interfaces;
using ASP.NET_MVC_VendaDeLanches.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASP.NET_MVC_VendaDeLanches.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILancheRepository _lancheRepository;

        public HomeController(ILancheRepository lancheRepository)
        {
            _lancheRepository = lancheRepository;
        }

        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                LanchesPreferidos = _lancheRepository.LanchesPreferidos
            };

            return View(homeViewModel);
        }
        
        public IActionResult Demo()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
