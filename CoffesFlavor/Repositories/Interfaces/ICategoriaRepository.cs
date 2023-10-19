using CoffesFlavor.Models;

namespace CoffesFlavor.Repositories.Interfaces
{
    public interface ICategoriaRepository
    {
        IEnumerable<Categoria> Categorias { get; }
    }
}
