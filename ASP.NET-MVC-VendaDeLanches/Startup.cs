﻿using ASP.NET_MVC_VendaDeLanches.Context;
using ASP.NET_MVC_VendaDeLanches.Models;
using ASP.NET_MVC_VendaDeLanches.Repositories;
using ASP.NET_MVC_VendaDeLanches.Repositories.Interfaces;
using ASP.NET_MVC_VendaDeLanches.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_MVC_VendaDeLanches;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 8;
            options.Password.RequiredUniqueChars = 1;
        });

        services.AddTransient<ILancheRepository, LancheRepository>();
        services.AddTransient<ICategoriaRepository, CategoriaRepository>();
        services.AddTransient<IPedidoRepository, PedidoRepository>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped(sp => CarrinhoCompra.GetCarrinho(sp));
        services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();

        services.AddControllersWithViews();

        services.AddMemoryCache();
        services.AddSession();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ISeedUserRoleInitial seedUserRoleInitial)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        seedUserRoleInitial.SeedRoles();
        seedUserRoleInitial.SeedUsers();

        app.UseSession();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");

            endpoints.MapControllerRoute(
                name: "categoriaFiltro",
                pattern: "Lanche/{action}/{categoria?}",
                defaults: new { controller = "Lanche", action = "List" });

            endpoints.MapDefaultControllerRoute();
        });
    }
}
