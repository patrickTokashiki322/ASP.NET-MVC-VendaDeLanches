using ASP.NET_MVC_VendaDeLanches.Models;

namespace ASP.NET_MVC_VendaDeLanches.ViewModels
{
    public class LancheListViewModel
    {
        public IEnumerable<Lanche> lanches { get; set; }
        public string CategoriaAtual { get; set; }
    }
}
