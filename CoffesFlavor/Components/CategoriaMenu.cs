using CoffesFlavor.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoffesFlavor.Components
{
    public class CategoriaMenu : ViewComponent
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaMenu(ICategoriaRepository repository)
        {
            _categoriaRepository = repository;
        }

        public IViewComponentResult Invoke()
        {
            var categorias = _categoriaRepository.Categorias.OrderBy(c => c.CategoriaNome);
            return View(categorias);
        }

    }
}
