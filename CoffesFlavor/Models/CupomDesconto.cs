using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffesFlavor.Models
{
    public class CupomDesconto
    {
        public int CupomDescontoId { get; set; }
        [StringLength(20)]
        public string Cupom { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Desconto { get; set; }
        public bool Ativo { get; set; }
    }
}
