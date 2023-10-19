using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffesFlavor.Models
{
    [Table("Produtos")]
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }

        [Required(ErrorMessage ="O nome do produto deve ser informado")]
        [Display(Name ="Nome do produto")]
        [StringLength(80, MinimumLength = 10, 
            ErrorMessage ="O {0} deve ter no minimo {1} e no maximo {2}")]
        public string Nome { get; set; }

        [Required(ErrorMessage = " A descrição do produto deve ser informado")]
        [Display(Name = "Descrição do produto")]
        [MinLength(20, ErrorMessage ="Descrição deve ter no minimo {1} caracteres")]
        [MaxLength(200, ErrorMessage = "Descrição deve ter no minimo {1} caracteres")]
        public string DescricaoCurta { get; set; }

        [Required(ErrorMessage = " A descrição detalhada do produto deve ser informado")]
        [Display(Name = "Descrição detalhada do produto")]
        [MinLength(20, ErrorMessage = "Descrição detalhada deve ter no minimo {1} caracteres")]
        [MaxLength(200, ErrorMessage = "Descrição detalhada deve ter no minimo {1} caracteres")]
        public string DescricaoDetalhada { get; set; }

        [Required(ErrorMessage ="Informe o preço do lanche")]
        [Display(Name ="Preço")]
        [Column(TypeName ="decimal(10,2)")]
        [Range(1, 999.99, ErrorMessage ="O preço deve ter no maximo entre 1 e 999,99")]
        public decimal Preco { get; set; }

        [Display(Name ="Caminho Imagem Normal")]
        [StringLength(200, ErrorMessage ="O {0} deve ter no máximo {1} caracteres")]
        public string ImagemUrl { get; set; }

        [Display(Name = "Caminho Imagem Miniatura")]
        [StringLength(200, ErrorMessage = "O {0} deve ter no máximo {1} caracteres")]
        public string ImagemThumbnailUrl { get; set; }

        [Display(Name ="Preferido?")]
        public bool IsLanchePreferido { get; set; }

        [Display(Name ="Estoque")]
        public bool EmEstoque { get; set; }


        //Propriedades de navegação
        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }



    }
}
