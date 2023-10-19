using CoffesFlavor.Context;
using CoffesFlavor.Models;
using CoffesFlavor.Repositories.Interfaces;

namespace CoffesFlavor.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context) 
        {
            _context = context;
        }
        
        public IEnumerable<Categoria> Categorias => _context.Categorias;

    }
}
