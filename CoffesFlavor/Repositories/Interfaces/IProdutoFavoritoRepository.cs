using CoffesFlavor.Models;
using Microsoft.AspNetCore.Identity;

namespace CoffesFlavor.Repositories.Interfaces
{
    public interface IProdutoFavoritoRepository
    {
        IEnumerable<ProdutoFavorito> _produtosFavorito { get; }
        IEnumerable<Produto> Produtos { get; }
        //IEnumerable<Produto> GetProdutosFavoritos();
    }
}
