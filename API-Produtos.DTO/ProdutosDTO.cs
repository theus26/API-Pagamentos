namespace API_Produtos.DTO
{
    public class ProdutosDTO
    {
          public long? Id { get; set; } 
          public string nome { get; set; }
          public float valor_unitario { get; set; }
          public int qtde_estoque { get; set; }
    }

}

