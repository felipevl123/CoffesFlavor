using CoffesFlavor.Context;
using CoffesFlavor.Models;
using CoffesFlavor.Repositories.Interfaces;
using CoffesFlavor.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace CoffesFlavor.Repositories
{
    public class ProdutoFavoritoRepository : IProdutoFavoritoRepository
    {
        private readonly AppDbContext _context;
        private readonly HttpServiceClaimPrincipalAccessor _principalAccessor;

        public IEnumerable<ProdutoFavorito> _produtosFavorito =>
            _context.ProdutosFavoritos
            .Where(p => p.IdentityUser.Id == _principalAccessor.GetClaim())
            .Include(p => p.Produto);

        public IEnumerable<Produto> Produtos =>
            _context.Produtos;


        public ProdutoFavoritoRepository(AppDbContext context, HttpServiceClaimPrincipalAccessor principalAccessor)
        {
            _context = context;
            _principalAccessor = principalAccessor;
        }

        //public IEnumerable<Produto> GetProdutosFavoritos()
        //{

        //}
    }
}
