using CoffesFlavor.Context;
using CoffesFlavor.Models;
using CoffesFlavor.Repositories.Interfaces;
using CoffesFlavor.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace CoffesFlavor.Repositories
{
    public class PedidosHistoricoRepository : IPedidosHistoricoRepository
    {

        private readonly AppDbContext _context;
        private readonly HttpServiceClaimPrincipalAccessor _principalAccessor;
        public IEnumerable<PedidosHistorico> PedidosHistorico =>
            _context.PedidosHistoricos
            .Include(p => p.Pedido)
            .ThenInclude(p => p.StatusPedido);


        public PedidosHistoricoRepository(AppDbContext context,
                HttpServiceClaimPrincipalAccessor principalAccessor)
        {
            _context = context;
            _principalAccessor = principalAccessor;
        }

        public IEnumerable<PedidosHistorico> GetHistoricoPedidos()
        {
            var userId = _principalAccessor.GetClaim();

            var resultado = PedidosHistorico
                .Where(p => p.AspNetUsersId == userId)
                .OrderByDescending(p => p.Pedido.PedidoEnviado);
            return resultado;
        }

        public Pedido GetDetalhePedido(int? id)
        {
            var userId = _principalAccessor.GetClaim();
            var lista = PedidosHistorico
                .Where(p => p.AspNetUsersId == userId
                                && p.PedidoId == id);

            if (lista.IsNullOrEmpty())
            {
                return null;
            }

            var pedido = _context.Pedidos
                        .Include(pd => pd.PedidoItens)
                        .ThenInclude(l => l.Produto)
                        .FirstOrDefault(p => p.PedidoId == id);
           
            return pedido;

        }

        public IEnumerable<PedidosHistorico> FilterByDate(DateTime? minDate, DateTime? maxDate)
        {
            var resultado = this.GetHistoricoPedidos();

            if (minDate.HasValue)
            {
                resultado = resultado.Where(x => x.Pedido.PedidoEnviado >= minDate.Value)
                    .OrderByDescending(p => p.Pedido.PedidoEnviado);
            }

            if (maxDate.HasValue)
            {
                resultado = resultado.Where(x => x.Pedido.PedidoEnviado <= maxDate.Value)
                    .OrderByDescending(p => p.Pedido.PedidoEnviado);
            }

            return resultado;
        }

        public IEnumerable<PedidosHistorico> FilterByProtocolo(string filter)
        {
            var resultado = this.GetHistoricoPedidos();

            var r = int.TryParse(filter, out int id);

            if(r != true)
            {
                return resultado;
            }

            resultado = resultado.Where(p => p.PedidoId == id)
                .OrderByDescending(p => p.Pedido.PedidoEnviado);


            return resultado;
        }

        public IEnumerable<PedidosHistorico> FilterByTotal(string filter)
        {
            var resultado = this.GetHistoricoPedidos();

            var r = decimal.TryParse(filter, out decimal preco);

            if (r != true)
            {
                return resultado;
            }

            resultado = resultado.Where(p => p.Pedido.PedidoTotal <= preco)
                .OrderByDescending(p => p.Pedido.PedidoTotal);


            return resultado;
        }
    }
}
