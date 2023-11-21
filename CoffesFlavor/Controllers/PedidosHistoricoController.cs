using CoffesFlavor.Context;
using CoffesFlavor.Models;
using CoffesFlavor.Repositories.Interfaces;
using CoffesFlavor.Services;
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
        private readonly IAvaliacaoPedidosRepository _avaPedidosRepository;
        private readonly UserSessionService _userSessionService;


        public PedidosHistoricoController(IPedidosHistoricoRepository pedidosHistoricoRepository,
            IAvaliacaoPedidosRepository avaPedidosRepository,
            UserSessionService userSessionService)
        {
            _pedidosHistoricoRepository = pedidosHistoricoRepository;
            _avaPedidosRepository = avaPedidosRepository;
            _userSessionService = userSessionService;
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

        [HttpGet]
        public IActionResult AvaliarEntrega(int id)
        {
            var pedido = _pedidosHistoricoRepository.GetDetalhePedido(id);

            if (_avaPedidosRepository.VerificaFeedBack(pedido))
            {
                return RedirectToAction(nameof(FeedBackDetalhe), pedido);
            }

            var avaliacaoPdVW = new AvaliacaoPedidoViewModel
            { 
                Pedido = pedido
              
            };

            //TempData["PedidoId"] = pedido.PedidoId;

            _userSessionService.SetData("PedidoId", pedido.PedidoId);

            return View(avaliacaoPdVW);

        }

        [HttpPost]
        public IActionResult AvaliarEntrega(AvaliacaoPedidoViewModel av)
        {
            //int pedidoId = (int)(TempData["PedidoId"]);
            var pedidoId = _userSessionService.GetData<int>("PedidoId");

            var pedido = _pedidosHistoricoRepository.GetDetalhePedido(pedidoId);

            av.Pedido = pedido;

            if (String.IsNullOrEmpty(av.Nota))
            {
                ModelState
                    .AddModelError("", "Preencha o campo de Nota");
            }

            if (ModelState.IsValid)
            {
                _avaPedidosRepository.AvaliaPedido(av);
                return RedirectToAction(nameof(Index));
            }

            return View(av);

        }

        public IActionResult FeedBackDetalhe(Pedido pedido)
        {
            var pedidoAvaliado = _avaPedidosRepository.BuscaAvaliacao(pedido);

            var avaliacao = new PedidoAvaliadoViewModel
            {
                NomeDoUsuario = pedidoAvaliado.Pedido.Nome,
                NumeroDoPedido = pedidoAvaliado.PedidoId,
                DataDeEntrega = (DateTime)pedidoAvaliado.Pedido.PedidoEntregueEm,
                FeedBack = pedidoAvaliado.FeedBackCliente,
                Nota = pedidoAvaliado.Nota
            };

            return View(avaliacao);
        }


    }
}
