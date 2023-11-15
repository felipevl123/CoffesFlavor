using System.ComponentModel.DataAnnotations;

namespace CoffesFlavor.ViewModels
{
    public class EsqueciSenhaViewModel
    {
        [Display(Name ="E-mail")]
        [Required(ErrorMessage ="O campo {0} é de preenchimento obrigatório")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
