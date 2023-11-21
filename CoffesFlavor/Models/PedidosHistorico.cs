using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffesFlavor.Models
{
    public class PedidosHistorico
    {
        public int PedidosHistoricoId { get; set; }
        public int PedidoId { get; set; }

        [StringLength(450)]
        public string AspNetUsersId { get; set; }

        // Propriedades de navegação
        public virtual IdentityUser IdentityUser { get; set; }
        public virtual Pedido Pedido { get; set; }
    }
}
