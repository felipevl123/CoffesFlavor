using CoffesFlavor.Models;
using CoffesFlavor.ViewModels;

namespace CoffesFlavor.Repositories.Interfaces
{
    public interface IAvaliacaoPedidosRepository
    {
        void AvaliaPedido(AvaliacaoPedidoViewModel avaliacaoPdVM);
        bool VerificaFeedBack(Pedido pedido);
        AvaliacaoPedido BuscaAvaliacao(Pedido pedido);
    }
}
