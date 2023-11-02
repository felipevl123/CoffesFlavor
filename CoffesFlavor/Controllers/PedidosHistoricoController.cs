using CoffesFlavor.Context;
using CoffesFlavor.Repositories.Interfaces;
using CoffesFlavor.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;

namespace CoffesFlavor.Controllers
{
    public class PedidosHistoricoController : Controller
    {
        private readonly IPedidosHistoricoRepository _pedidosHistoricoRepository;

        public PedidosHistoricoController(IPedidosHistoricoRepository pedidosHistoricoRepository)
        {
            _pedidosHistoricoRepository = pedidosHistoricoRepository;
        }

        public IActionResult Index()
        {

            if (User.Identity.IsAuthenticated)
            {
                var resultado = _pedidosHistoricoRepository.GetPedidos();
                var pedidoHistViewModel = new PedidosListHistoricoViewModel
                {
                    PedidosHistorico = resultado,
                };
                return View(pedidoHistViewModel);

    }
            return RedirectToAction("Login", "Account");

        }
    }
}
