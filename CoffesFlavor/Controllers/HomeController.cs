using CoffesFlavor.Models;
using CoffesFlavor.Repositories.Interfaces;
using CoffesFlavor.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CoffesFlavor.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProdutoRepository _produtoRepository;

        public HomeController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel()
            {
                ProdutosPreferidos = _produtoRepository.ProdutosPreferidos
            };

            //TempData["Nome"] = "Macoratti";
            return View(homeViewModel);
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}