using CoffesFlavor.Models;

namespace CoffesFlavor.ViewModels
{
    public class PedidoProdutoViewModel
    {
        public Pedido Pedido { get; set; }
        public IEnumerable<PedidoDetalhe> PedidoDetalhe { get; set; }
    }
}
