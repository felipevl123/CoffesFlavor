using CoffesFlavor.Context;
using CoffesFlavor.Models;
using CoffesFlavor.Repositories.Interfaces;

namespace CoffesFlavor.Repositories
{
    public class CupomDescontoRepository : ICupomDescontoRepository
    {
        private readonly AppDbContext _context;

        public CupomDescontoRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public CupomDesconto ValidaCupom(string cupom)
        {
                          
           return _context.CupomDesconto
                    .Where(c => c.Cupom == cupom.ToUpper().Trim()
                && c.Ativo == true)
                    .FirstOrDefault();
        }

        public decimal AtualizaDesconto(Pedido pedido, CupomDesconto cupom)
        {
            var valorDesconto = pedido.PedidoTotal * cupom.Desconto;
            pedido.PedidoTotal -= valorDesconto;

            _context.PedidosComDesconto
                .Add(new PedidosComDesconto {
                    PedidoId = pedido.PedidoId,
                    CupomDescontoId = cupom.CupomDescontoId
                });

            _context.SaveChanges();

            return valorDesconto;
        }
    }
}
