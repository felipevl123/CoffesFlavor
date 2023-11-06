using CoffesFlavor.Models;

namespace CoffesFlavor.Repositories.Interfaces
{
    public interface IPedidosHistoricoRepository
    {
        IEnumerable<PedidosHistorico> PedidosHistorico {  get; }
        IEnumerable<PedidosHistorico> GetHistoricoPedidos();
        Pedido GetDetalhePedido(int? id);
        IEnumerable<PedidosHistorico> FilterByDate(DateTime? minDate, DateTime? maxDate);
        IEnumerable<PedidosHistorico> FilterByProtocolo(string filter);
        IEnumerable<PedidosHistorico> FilterByTotal(string filter);
    }
}
