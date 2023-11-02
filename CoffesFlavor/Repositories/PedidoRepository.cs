using CoffesFlavor.Context;
using CoffesFlavor.Models;
using CoffesFlavor.Repositories.Interfaces;
using CoffesFlavor.Services;
using System.Security.Claims;

namespace CoffesFlavor.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _context;
        private readonly CarrinhoCompra _carrinhoCompra;
        private readonly HttpServiceClaimPrincipalAccessor _principalAccessor;


        public PedidoRepository(AppDbContext context, CarrinhoCompra carrinhoCompra, 
            HttpServiceClaimPrincipalAccessor principalAccessor)
        {
            _context = context;
            _carrinhoCompra = carrinhoCompra;
            _principalAccessor = principalAccessor;
        }

        public void CriaPedido(Pedido pedido)
        {
            var userId = _principalAccessor.GetClaim();
            pedido.PedidoEnviado = DateTime.Now;
            _context.PedidosHistoricos
                .Add(new PedidosHistorico
                {
                    Pedido = pedido,
                    AspNetUsersId = userId
                });
            _context.Pedidos.Add(pedido);
            _context.SaveChanges();
            // Implementar Adicionar a PedidosHistorico o pedido criado

            var carrinhoCompraItens = _carrinhoCompra.CarrinhoCompraItens;

            foreach (var carrinhoItem in carrinhoCompraItens)
            {
                var pedidoDetail = new PedidoDetalhe
                {
                    Quantidade = carrinhoItem.Quantidade,
                    ProdutoId = carrinhoItem.Produto.ProdutoId,
                    PedidoId = pedido.PedidoId,
                    Preco = carrinhoItem.Produto.Preco,
                };
                _context.PedidoDetalhes.Add(pedidoDetail);
            }

            _context.SaveChanges();
            
        }
    }
}
