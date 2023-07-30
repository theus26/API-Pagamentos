using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Produtos.DAL.Entities
{
    public class Vendas
    {
        [Key]
        public int IdVenda { get; set; }
        [Required]
        public int IdProduto { get; set; }
        [Required]
        public string nome { get; set; }
        [Required]
        public float valor_unitario { get; set; }
        [Required]
        public int qtde_estoque { get; set; }
        [Required]
        public DateTime DataUltimaVenda { get; set; }
        [Required]
        public float ValorUltimaVenda { get; set; }

        [ForeignKey("IdProduto")]
        public virtual Produto produto { get; set; }
    }
}
