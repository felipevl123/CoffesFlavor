using CoffesFlavor.Context;
using CoffesFlavor.Models;
using CoffesFlavor.Repositories.Interfaces;
using CoffesFlavor.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
                var resultado = _pedidosHistoricoRepository.GetHistoricoPedidos();
                var pedidoHistViewModel = new PedidosListHistoricoViewModel
                {
                    PedidosHistorico = resultado,
                };
                return View(pedidoHistViewModel);

            }
            return RedirectToAction("Login", "Account");

        }

        public IActionResult Details(int? id)
        {
            var pedido = _pedidosHistoricoRepository.GetDetalhePedido(id);

            if (pedido == null)
            {
                Response.StatusCode = 404;
                return View("PedidoNotFound", id.Value);
            }

            PedidoProdutoViewModel pedidoProdutos = new PedidoProdutoViewModel()
            {
                Pedido = pedido,
                PedidoDetalhe = pedido.PedidoItens
            };
            return View(pedidoProdutos);
        }

        public IActionResult FiltrarPorData(DateTime? minDate,
            DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }

            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

            var resultado = _pedidosHistoricoRepository.FilterByDate(minDate, maxDate);

            var pedidoHistViewModel = new PedidosListHistoricoViewModel
            {
                PedidosHistorico = resultado,
            };

            return View("Index", pedidoHistViewModel);
        }

        public IActionResult FiltrarPorNumero(string filter)
        {

            if (filter.IsNullOrEmpty())
            {
                return RedirectToAction("Index");
            }
            
            filter.Replace(" ", "");

            var resultado = _pedidosHistoricoRepository.FilterByProtocolo(filter);

            var pedidoHistViewModel = new PedidosListHistoricoViewModel
            {
                PedidosHistorico = resultado,
            };

            return View("Index", pedidoHistViewModel);
        }

        public IActionResult FiltrarPorPreco(string filter)
        {

            if (filter.IsNullOrEmpty())
            {
                return RedirectToAction("Index");
            }

            filter.Replace(" ", "");
            filter.Replace(",", ".");

            var resultado = _pedidosHistoricoRepository.FilterByTotal(filter);

            var pedidoHistViewModel = new PedidosListHistoricoViewModel
            {
                PedidosHistorico = resultado,
            };

            return View("Index", pedidoHistViewModel);
        }
    }
}
