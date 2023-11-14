using CoffesFlavor.Repositories.Interfaces;
using CoffesFlavor.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CoffesFlavor.Controllers
{
    public class ContaDetalhesController : Controller
    {
        private readonly IContaDetalhesRepository _contaDetalhes;

        public ContaDetalhesController(IContaDetalhesRepository contaDetalhes)
        {
            _contaDetalhes = contaDetalhes;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var contaDetalheViewModel = new ContaDetalheViewModel
                {
                    Conta = _contaDetalhes.GetContaLogada()
                };
                return View(contaDetalheViewModel);

            }
            return RedirectToAction("Login", "Account");
        }
    }
}
