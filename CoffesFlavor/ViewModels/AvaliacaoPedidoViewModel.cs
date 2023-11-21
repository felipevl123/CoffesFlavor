using CoffesFlavor.Models;
using System.ComponentModel.DataAnnotations;

namespace CoffesFlavor.ViewModels
{
    public class AvaliacaoPedidoViewModel
    {
        public Pedido Pedido { get; set; }
        [Display(Name = "Avaliação")]
        [StringLength(450)]
        public string FeedBack { get; set; }
        [Required(ErrorMessage="Necessario avaliar o pedido")]
        public string Nota { get; set; }
    }
}
