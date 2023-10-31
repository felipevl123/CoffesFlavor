using CoffesFlavor.Context;
using CoffesFlavor.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffesFlavor.Areas.Admin.Servicos
{
    public class RelatorioVendaService
    {
        private readonly AppDbContext _context;

        public RelatorioVendaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pedido>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var resultado = from obj in _context.Pedidos select obj;


            if (minDate.HasValue)
            {
                resultado = resultado.Where(x => x.PedidoEnviado >= minDate.Value);
            }

            if (maxDate.HasValue)
            {
                resultado = resultado.Where(x => x.PedidoEnviado <= maxDate.Value);
            }

            return await resultado
                .Include(p => p.PedidoItens)
                .ThenInclude(p => p.Produto)
                .OrderByDescending(x => x.PedidoEnviado)
                .ToListAsync();
        }
    }
}
