using Microsoft.AspNetCore.Identity;

namespace CoffesFlavor.Models
{
    public class ProdutoFavorito
    {
        public int ProdutoFavoritoId { get; set; }
        public int ProdutoId { get; set; }


        public virtual Produto Produto { get; set; }
        public virtual IdentityUser IdentityUser { get; set; }
    }
}
