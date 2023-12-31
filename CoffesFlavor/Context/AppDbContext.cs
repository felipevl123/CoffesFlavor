﻿using CoffesFlavor.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoffesFlavor.Context
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoDetalhe> PedidoDetalhes { get; set; }
        public DbSet<PedidosHistorico> PedidosHistoricos { get; set; }
        public DbSet<StatusPedido> StatusPedido { get; set; }
        public DbSet<ContaDetalhe> ContaDetalhes { get; set; }
        public DbSet<CupomDesconto> CupomDesconto { get; set; }
        public DbSet<PedidosComDesconto> PedidosComDesconto { get; set; }
        public DbSet<AvaliacaoPedido> AvaliacaoPedidos { get; set; }
        public DbSet<ProdutoFavorito> ProdutosFavoritos { get; set; }
    }
}
