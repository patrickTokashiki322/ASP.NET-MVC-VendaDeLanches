using ASP.NET_MVC_VendaDeLanches.Context;
using ASP.NET_MVC_VendaDeLanches.Models;
using ASP.NET_MVC_VendaDeLanches.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_MVC_VendaDeLanches.Repositories
{
    public class LancheRepository : ILancheRepository
    {
        private readonly AppDbContext _context;

        public LancheRepository(AppDbContext contexto)
        {
            _context = contexto;
        }

        public IEnumerable<Lanche> Lanches => _context.Lanches.Include(c => c.Categoria);

        public IEnumerable<Lanche> LanchesPreferidos => _context.Lanches.Where(p => p.IsLanchePreferido).Include(c => c.Categoria);

        public Lanche GetLancheById(int lancheId) => _context.Lanches.FirstOrDefault(I => I.LancheId == lancheId);
    }
}
