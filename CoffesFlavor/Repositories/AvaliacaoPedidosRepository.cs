using CoffesFlavor.Context;
using CoffesFlavor.Models;
using CoffesFlavor.Repositories.Interfaces;
using CoffesFlavor.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CoffesFlavor.Repositories
{
    public class AvaliacaoPedidosRepository : IAvaliacaoPedidosRepository
    {
        private readonly AppDbContext _context;

        public AvaliacaoPedidosRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AvaliaPedido(AvaliacaoPedidoViewModel avaliacaoPdVM)
        {
            var avaliacaoPedido = new AvaliacaoPedido
            {
                PedidoId = avaliacaoPdVM.Pedido.PedidoId,
                FeedBackCliente = avaliacaoPdVM.FeedBack
            };

            _context.AvaliacaoPedidos.Add(avaliacaoPedido);
            _context.SaveChanges();
        }

        public bool VerificaFeedBack(Pedido pedido)
        {
            return _context.AvaliacaoPedidos.Any(p => p.PedidoId == pedido.PedidoId);
        }

        public AvaliacaoPedido BuscaAvaliacao(Pedido pedido)
        {
            return _context.AvaliacaoPedidos
                .Include(p => p.Pedido)
                .FirstOrDefault(p => p.PedidoId == pedido.PedidoId);
        }
    }
}
