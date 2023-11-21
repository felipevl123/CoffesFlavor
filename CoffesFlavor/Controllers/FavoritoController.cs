using CoffesFlavor.Context;
using CoffesFlavor.Models;
using CoffesFlavor.Repositories.Interfaces;
using CoffesFlavor.Services;
using CoffesFlavor.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoffesFlavor.Controllers
{
    public class FavoritoController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly HttpServiceClaimPrincipalAccessor _principalAccessor;
        private readonly IProdutoFavoritoRepository _produtoFavoritoRepository;

        public List<ProdutoFavorito> _produtosFav =>
            _context.ProdutosFavoritos
                .Where(p => p.IdentityUser.Id == _principalAccessor.GetClaim())
                .Include(p => p.Produto)
                .ToList();

        public FavoritoController(AppDbContext context, 
            UserManager<IdentityUser> userManager, 
            HttpServiceClaimPrincipalAccessor principalAccessor, 
            IProdutoFavoritoRepository produtoFavoritoRepository)
        {
            _context = context;
            _userManager = userManager;
            _principalAccessor = principalAccessor;
            _produtoFavoritoRepository = produtoFavoritoRepository;
        }

        public IActionResult Index()
        {
            var produtos = new List<Produto>();
            foreach(var item in _produtosFav)
            {
                if(_context.Produtos.Contains(item.Produto))
                {
                    produtos.Add(item.Produto);
                }
            }

            var produtoListViewModel = new ProdutoListViewModel
            {
                Produtos = produtos,
                CategoriaAtual = "Todos os produtos"
            };

            return View(produtoListViewModel);
        }

        [Authorize]
        public IActionResult AdicionarItemAosFavoritos(int produtoId)
        {
            var idUser = _principalAccessor.GetClaim();
            var usuario = _userManager.Users.Where(u => u.Id == idUser).FirstOrDefault();


            var produto = new ProdutoFavorito
            {
                ProdutoId = produtoId,
                IdentityUser = usuario
            };

            var resultProduto = _produtosFav.Any(p => p.ProdutoId == produtoId);
            var resultUser = _produtosFav.Any(p => p.IdentityUser.Id == usuario.Id);

            //var result = _produtosFav.FirstOrDefault().ProdutoId == produtoId
            //    && _produtosFav.FirstOrDefault().IdentityUser.Id == usuario.Id;

            if (resultProduto && resultUser)
            {
                TempData["Alerta"] = "Seu Produto já esta adicionado aos favoritos";
                return RedirectToAction("Index", "Favorito");
            }

            _context.ProdutosFavoritos.Add(produto);
            _context.SaveChanges();

            return RedirectToAction("Index", "Favorito");


        }

        [Authorize]
        public IActionResult RemoverItemDosFavoritos(int produtoId)
        {
            var idUser = _principalAccessor.GetClaim();
            var usuario = _userManager.Users.Where(u => u.Id == idUser).FirstOrDefault();


            var produto = new ProdutoFavorito
            {
                ProdutoId = produtoId,
                IdentityUser = usuario
            };

            var resultProduto = _produtosFav.Any(p => p.ProdutoId == produtoId);
            var resultUser = _produtosFav.Any(p => p.IdentityUser.Id == usuario.Id);

            //var result = _produtosFav.FirstOrDefault().ProdutoId == produtoId
            //    && _produtosFav.FirstOrDefault().IdentityUser.Id == usuario.Id;

            if (resultProduto && resultUser)
            {
                var f = _context.ProdutosFavoritos
                    .Where(p => p.Produto.ProdutoId == produtoId
                    && p.IdentityUser.Id == usuario.Id).ToList();

                foreach (var item in f)
                {
                    _context.ProdutosFavoritos.Remove(item);
                }
                _context.SaveChanges();
                TempData["Alerta"] = "Seu produto foi removido dos favoritos";
                return RedirectToAction("Index", "Favorito");
            }

            return RedirectToAction("Index", "Favorito");


        }


    }
}
