namespace CoffesFlavor.Models
{
    public class PedidosComDesconto
    {
        public int PedidosComDescontoId { get; set; }
        public int PedidoId { get; set; }
        public int CupomDescontoId { get; set; }

        public virtual Pedido Pedido { get; set; }
        public virtual CupomDesconto CupomDesconto { get; set; }
    }
}
