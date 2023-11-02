using CoffesFlavor.Models;

namespace CoffesFlavor.Repositories.Interfaces
{
    public interface IPedidosHistoricoRepository
    {
        IEnumerable<PedidosHistorico> PedidosHistorico {  get; }
        IEnumerable<PedidosHistorico> GetPedidos();
    }
}
