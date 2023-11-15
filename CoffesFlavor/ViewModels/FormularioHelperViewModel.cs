using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CoffesFlavor.ViewModels
{
    public class FormularioHelperViewModel
    {
        [Required(ErrorMessage ="Numero do pedido é obrigatório")]
        [DisplayName("N° do pedido")]
        public string NumeroDoPedido { get; set; }
        [Required(ErrorMessage = "Descrição da solicitação")]
        [DisplayName("Descrição")]
        public string Descricao { get; set; }
    }
}
