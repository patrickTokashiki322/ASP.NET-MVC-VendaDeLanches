﻿using ASP.NET_MVC_VendaDeLanches.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_MVC_VendaDeLanches.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Lanche> Lanches { get; set; }
        public DbSet<CarrinhoCompraItem> CarrinhoCompraItems { get; set; }
    }
}