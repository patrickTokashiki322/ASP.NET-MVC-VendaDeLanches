using ASP.NET_MVC_VendaDeLanches.Models;

namespace ASP.NET_MVC_VendaDeLanches.Repositories.Interfaces
{
    public interface IPedidoRepository
    {
        void CriarPedido(Pedido pedido);
    }
}
