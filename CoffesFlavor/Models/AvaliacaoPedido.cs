using System.ComponentModel.DataAnnotations;

namespace CoffesFlavor.Models
{
    public class AvaliacaoPedido
    {
        public int AvaliacaoPedidoId { get; set; }
        public int PedidoId { get; set; }
        [StringLength(450)]
        public string FeedBackCliente { get; set; }

        public virtual Pedido Pedido { get; set;}
    }
}
