namespace API_Produtos.DTO
{
    public class PurchaseProductsDTO
    {
        public int produto_id { get; set; }
        public int qtde_comprada { get; set; }
        public Cartao cartao { get; set; }
    }

    public class Cartao
    {
        public string titular { get; set; }
        public string numero { get; set; }
        public string data_expiracao { get; set; }
        public string bandeira { get; set; }
        public string cvv { get; set; }
    }

}

