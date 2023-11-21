using CoffesFlavor.Models;

namespace CoffesFlavor.ViewModels
{
    public class ProdutoFavoritoViewModel
    {
        public IEnumerable<Produto> Produtos { get; set; }
        public string CategoriaAtual { get; set; }
        public IEnumerable<ProdutoFavorito> ProdutosFavoritos { get; set; }
    }
}
