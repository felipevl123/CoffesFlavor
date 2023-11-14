using CoffesFlavor.Context;
using CoffesFlavor.Models;
using CoffesFlavor.Repositories.Interfaces;
using CoffesFlavor.Services;
using Microsoft.EntityFrameworkCore;

namespace CoffesFlavor.Repositories
{
    public class ContaDetalheRepository : IContaDetalhesRepository
    {
        private readonly AppDbContext _context;
        private readonly HttpServiceClaimPrincipalAccessor _principalAccessor;
        public IEnumerable<ContaDetalhe> ContasDetalhesUsuarios =>
            _context.ContaDetalhes
            .Include(u => u.IdentityUser);

        public ContaDetalheRepository
            (AppDbContext context, HttpServiceClaimPrincipalAccessor principalAccessor)
        {
            _context = context;
            _principalAccessor = principalAccessor;
        }

        public ContaDetalhe GetContaLogada()
        {
            var userId = _principalAccessor.GetClaim();

            return ContasDetalhesUsuarios
                .FirstOrDefault(c => c.AspNetUsersId == userId);
        }
    }
}
