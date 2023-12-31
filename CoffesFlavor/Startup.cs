﻿using CoffesFlavor.Areas.Admin.Servicos;
using CoffesFlavor.Context;
using CoffesFlavor.Models;
using CoffesFlavor.Repositories;
using CoffesFlavor.Repositories.Interfaces;
using CoffesFlavor.Services;
using CoffesFlavor.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;

namespace CoffesFlavor;
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
        services.AddDbContext<AppDbContext>(options => 
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        services.AddIdentity<IdentityUser, IdentityRole>(options =>
        options.SignIn.RequireConfirmedEmail = true)
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<ConfigurationImagens>(Configuration.GetSection("ConfigurationPastaImagens"));

        //services.Configure<GmailSettings>(Configuration.GetSection(nameof (GmailSettings)));
        services.Configure<SendGridSettings>(Configuration.GetSection(nameof(SendGridSettings)));

        services.AddTransient<IProdutoRepository, ProdutoRepository>();
        services.AddTransient<ICategoriaRepository, CategoriaRepository>();
        services.AddTransient<IPedidoRepository, PedidoRepository>();
        services.AddTransient<IPedidosHistoricoRepository, PedidosHistoricoRepository>();
        services.AddTransient<IContaDetalhesRepository, ContaDetalheRepository>();
        services.AddTransient<ICupomDescontoRepository, CupomDescontoRepository>();
        services.AddTransient<IAvaliacaoPedidosRepository, AvaliacaoPedidosRepository>();
        services.AddTransient<IProdutoFavoritoRepository, ProdutoFavoritoRepository>();
        services.AddTransient<HttpServiceClaimPrincipalAccessor>();
        services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();
        //services.AddSingleton<IEmailService, GmailService>();
        services.AddSingleton<IEmailService, SendGridService>();

        services.AddScoped<RelatorioVendaService>();
        services.AddScoped<GraficoVendasService>();

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", politica =>
            {
                politica.RequireRole("Admin");
            });
        });

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<UserSessionService>();
        services.AddScoped(sp => CarrinhoCompra.GetCarrinho(sp));

        services.AddControllersWithViews();

        services.AddPaging(options =>
        {
            options.ViewName = "Bootstrap4";
            options.PageParameterName = "pageindex";
        });


        services.AddMemoryCache();
        services.AddSession();

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env
        , ISeedUserRoleInitial seedUserRoleInitial)
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

        // cria os perfis
        seedUserRoleInitial.SeedRoles();
        // cria os usuários e atribui ao perfil
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
                pattern: "Produto/{action}/{categoria?}",
                defaults: new { Controller = "Produto", action = "List"});


            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}