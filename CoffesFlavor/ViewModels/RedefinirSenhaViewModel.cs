using System.ComponentModel.DataAnnotations;

namespace CoffesFlavor.ViewModels
{
    public class RedefinirSenhaViewModel
    {
        [Required]
        public string Token { get; set; }

        [Display(Name ="E-mail")]
        [Required(ErrorMessage ="O campo {0} é de preenchimento obrigatório.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Nova senha")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        [DataType(DataType.Password)]
        public string NovaSenha { get; set; }

        [Display(Name = "Nova senha")]
        [Required(ErrorMessage = "O campo {0} é de preenchimento obrigatório.")]
        [DataType(DataType.Password)]
        public string ConfNovaSenha { get; set; }
    }
}
