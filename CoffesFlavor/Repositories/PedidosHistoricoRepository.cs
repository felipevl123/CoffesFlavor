using CoffesFlavor.Context;
using CoffesFlavor.Models;
using CoffesFlavor.Repositories.Interfaces;
using CoffesFlavor.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CoffesFlavor.Repositories
{
    public class PedidosHistoricoRepository : IPedidosHistoricoRepository
    {

        private readonly AppDbContext _context;
        private readonly HttpServiceClaimPrincipalAccessor _principalAccessor;
        public IEnumerable<PedidosHistorico> PedidosHistorico =>
            _context.PedidosHistoricos.Include(p => p.Pedido);


        public PedidosHistoricoRepository(AppDbContext context,
                HttpServiceClaimPrincipalAccessor principalAccessor)
        {
            _context = context;
            _principalAccessor = principalAccessor;
        }

        public IEnumerable<PedidosHistorico> GetPedidos()
        {
            var userId = _principalAccessor.GetClaim();

            var resultado = PedidosHistorico
                .Where(p => p.AspNetUsersId == userId)
                .OrderBy(p => p.Pedido.PedidoEnviado);
            return resultado;
        }
    }
}
