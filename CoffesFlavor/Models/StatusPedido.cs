using System.ComponentModel.DataAnnotations;

namespace CoffesFlavor.Models
{
    public class StatusPedido
    {
        public int StatusPedidoId { get; set; }
        [StringLength(100, ErrorMessage = "O tamanho máximo é 100 caracteres")]
        [Required(ErrorMessage = "Informe o status do pedido")]
        [Display(Name = "Status do pedido")]
        public string Status { get; set; }
    }
}
