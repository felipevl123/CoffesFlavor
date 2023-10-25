using CoffesFlavor.Context;
using CoffesFlavor.Models;
using CoffesFlavor.Repositories.Interfaces;

namespace CoffesFlavor.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _context;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoRepository(AppDbContext context, CarrinhoCompra carrinhoCompra)
        {
            _context = context;
            _carrinhoCompra = carrinhoCompra;
        }

        public void CriaPedido(Pedido pedido)
        {
            pedido.PedidoEnviado = DateTime.Now;
            _context.Pedidos.Add(pedido);
            _context.SaveChanges();

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
