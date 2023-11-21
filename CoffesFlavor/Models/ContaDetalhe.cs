using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CoffesFlavor.Models
{
    public class ContaDetalhe
    {
        public int ContaDetalheId { get; set; }

        [StringLength(450)]
        public string AspNetUsersId { get; set; }

        public DateTime DataDeNascimento { get; set; }
        
        public string Genero { get; set; }
        [StringLength(200)]
        public string EndereçoCompleto { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }


        public virtual IdentityUser IdentityUser { get; set; }

    }
}