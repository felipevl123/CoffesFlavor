using CoffesFlavor.Models;
using CoffesFlavor.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffesFlavor.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoController(IPedidoRepository pedidoRepository, 
            CarrinhoCompra carrinhoCompra)
        {
            _pedidoRepository = pedidoRepository;
            _carrinhoCompra = carrinhoCompra;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Checkout (Pedido pedido)
        {
            int totalItensPedido = 0;
            decimal precoTotalPedido = 0.0m;

            //obtem os itens do Carrinho de Compra do cliente
            List<CarrinhoCompraItem> items = _carrinhoCompra.GetCarrinhoCompraItens();
            _carrinhoCompra.CarrinhoCompraItens = items;

            //verifica se existem itens de pedido
            if(_carrinhoCompra.CarrinhoCompraItens.Count == 0)
            {
                ModelState
                    .AddModelError("", "Seu carrinho está vazio, que tal incluir um produto...");
            }
            
            // calcula o total de itens e o total do pedido
            foreach(var item in items)
            {
                totalItensPedido += item.Quantidade;
                precoTotalPedido += (item.Produto.Preco * item.Quantidade);
            }

            // atribuir os valores obtidos ao pedido
            pedido.TotalItensPedido = totalItensPedido;
            pedido.PedidoTotal = precoTotalPedido;

            // valida os dados do pedido

            if (ModelState.IsValid)
            {
                // criar o pedido e os detalhes
                _pedidoRepository.CriaPedido(pedido);

                // define mensagens ao cliente
                ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu pedido :)";
                ViewBag.TotalPedido = _carrinhoCompra.GetCarrinhoCompraTotal();

                // limpa o carrinho
                _carrinhoCompra.LimparCarrinho();

                // exibhe a view com os dados do cliente e do pedido
                return View("~/Views/Pedido/CheckoutCompleto.cshtml", pedido);
            }
            return View(pedido);
        }
    }
}
