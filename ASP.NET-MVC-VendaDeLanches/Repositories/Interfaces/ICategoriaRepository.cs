using ASP.NET_MVC_VendaDeLanches.Models;

namespace ASP.NET_MVC_VendaDeLanches.Repositories.Interfaces
{
    public interface ICategoriaRepository
    {
        IEnumerable<Categoria> Categorias { get; }
    }
}
