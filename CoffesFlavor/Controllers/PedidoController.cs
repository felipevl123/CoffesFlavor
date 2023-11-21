using CoffesFlavor.Models;
using CoffesFlavor.Repositories.Interfaces;
using CoffesFlavor.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffesFlavor.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly CarrinhoCompra _carrinhoCompra;
        private readonly ICupomDescontoRepository _cupomDescontoRepository;

        public PedidoController(IPedidoRepository pedidoRepository,
            CarrinhoCompra carrinhoCompra,
            ICupomDescontoRepository cupomDescontoRepository)
        {
            _pedidoRepository = pedidoRepository;
            _carrinhoCompra = carrinhoCompra;
            _cupomDescontoRepository = cupomDescontoRepository;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Checkout (PedidoCompletoViewModel pedidoCompletoVW)
        {
            int totalItensPedido = 0;
            decimal precoTotalPedido = 0.0m;
            var cupomDesconto = new CupomDesconto();

            // Validar se existe Cupom de desconto referente ao que o usuário informou
            if (!String.IsNullOrEmpty(pedidoCompletoVW.Cupom))
            {
                cupomDesconto = _cupomDescontoRepository.ValidaCupom(pedidoCompletoVW.Cupom);
                if (cupomDesconto == null)
                {
                    ModelState
                        .AddModelError("Cupom", "Seu cupom de desconto não é valido");
                }
            }
             


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
            pedidoCompletoVW.Pedido.TotalItensPedido = totalItensPedido;
            pedidoCompletoVW.Pedido.PedidoTotal = precoTotalPedido;

            // valida os dados do pedido

            if (ModelState.IsValid)
            {
                // criar o pedido e os detalhes
                _pedidoRepository.CriaPedido(pedidoCompletoVW.Pedido);

                // Caso exista cupom, diminuir o total do pedido com base no cupom guardado no banco
                if (!String.IsNullOrEmpty(pedidoCompletoVW.Cupom))
                {
                    var valorDescontado = _cupomDescontoRepository.AtualizaDesconto(pedidoCompletoVW.Pedido, cupomDesconto);
                    ViewBag.ValorDescontado = valorDescontado;
                    //ViewData["ValorDescontado"] = valorDescontado;
                }

                // define mensagens ao cliente
                ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu pedido :)";
                //ViewBag.TotalPedido = _carrinhoCompra.GetCarrinhoCompraTotal();
                ViewBag.TotalPedido = pedidoCompletoVW.Pedido.PedidoTotal;

                // limpa o carrinho
                _carrinhoCompra.LimparCarrinho();

                // exibhe a view com os dados do cliente e do pedido
                return View("~/Views/Pedido/CheckoutCompleto.cshtml", pedidoCompletoVW.Pedido);
            }
            return View(pedidoCompletoVW);
        }
    }
}
