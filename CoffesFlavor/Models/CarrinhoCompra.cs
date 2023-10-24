using CoffesFlavor.Context;
using Microsoft.EntityFrameworkCore;

namespace CoffesFlavor.Models
{
    public class CarrinhoCompra
    {
        private readonly AppDbContext _context;

        public string CarrinhoCompraId { get; set; }
        public List<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }

        public CarrinhoCompra(AppDbContext context)
        {
            _context = context;
        }

        public static CarrinhoCompra GetCarrinho(IServiceProvider services)
        {
            //DEFINE UMA SESSÃO
            ISession session = 
                services.GetRequiredService<IHttpContextAccessor>()? .HttpContext.Session;

            //OBTEM UM SERVIÇO DO TIPO DO NOSSO CONTEXTO
            var context = services.GetService<AppDbContext>();

            //OBTEM OU GERA O ID DO CARRINHO
            string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

            //ATRIBUI O ID DO CARRINHO NA SESSÃO
            session.SetString("CarrinhoId", carrinhoId);

            //RETORNA O CARRINHO COM O CONTEXTO E O ID ATRIBUIDO OU OBTIDO
            return new CarrinhoCompra(context)
            {
                CarrinhoCompraId = carrinhoId
            };
        }

        public void AdicionarAoCarrinho(Produto produto)
        {
            var carrinhoCompraItem = _context.CarrinhoCompraItens
                .SingleOrDefault(s => s.Produto.ProdutoId == produto.ProdutoId &&
                s.CarrinhoCompraId == CarrinhoCompraId);

            if(carrinhoCompraItem == null)
            {
                carrinhoCompraItem = new CarrinhoCompraItem
                {
                    CarrinhoCompraId = CarrinhoCompraId,
                    Produto = produto,
                    Quantidade = 1
                };
               _context.CarrinhoCompraItens.Add(carrinhoCompraItem);
            }
            else
            {
                carrinhoCompraItem.Quantidade++;
            }
            _context.SaveChanges();

        }

        public int RemoverDoCarrinho(Produto produto)
        {
            var carrinhoCompraItem = _context.CarrinhoCompraItens
                .SingleOrDefault(s => s.Produto.ProdutoId == produto.ProdutoId &&
                s.CarrinhoCompraId == CarrinhoCompraId);

            var quantidadeLocal = 0;

            if(carrinhoCompraItem != null)
            {
                if(carrinhoCompraItem.Quantidade > 1)
                {
                    carrinhoCompraItem.Quantidade--;
                    quantidadeLocal = carrinhoCompraItem.Quantidade;
                }
                else
                {
                    _context.CarrinhoCompraItens.Remove(carrinhoCompraItem);
                }
            }

            _context.SaveChanges();
            return quantidadeLocal;
        }

        public List<CarrinhoCompraItem> GetCarrinhoCompraItens()
        {
            return CarrinhoCompraItens ??
                (CarrinhoCompraItens = _context.CarrinhoCompraItens
                .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                .Include(s => s.Produto)
                .ToList());
        }

        public void LimparCarrinho()
        {
            var carrinhoItens = _context.CarrinhoCompraItens
                    .Where(carrinho => carrinho.CarrinhoCompraId == CarrinhoCompraId);
            _context.CarrinhoCompraItens.RemoveRange(carrinhoItens);
            _context.SaveChanges();
        }

        public decimal GetCarrinhoCompraTotal()
        {
            var total = _context.CarrinhoCompraItens
                .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                .Select(c => c.Produto.Preco * c.Quantidade).Sum();

            return total;
        }




    }
}
