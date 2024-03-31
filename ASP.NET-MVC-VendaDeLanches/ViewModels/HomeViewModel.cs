using ASP.NET_MVC_VendaDeLanches.Models;

namespace ASP.NET_MVC_VendaDeLanches.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Lanche> LanchesPreferidos { get; set; }
    }
}
