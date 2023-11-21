using CoffesFlavor.Models;

namespace CoffesFlavor.Repositories.Interfaces
{
    public interface ICupomDescontoRepository
    {
        CupomDesconto ValidaCupom(string cupom);
        decimal AtualizaDesconto(Pedido pedido, CupomDesconto cupom);
    }
}
