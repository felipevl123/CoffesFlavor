using CoffesFlavor.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoffesFlavor.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public IActionResult List()
        {
            var produtos = _produtoRepository.Produtos;
            return View(produtos);
        }






    }
}
