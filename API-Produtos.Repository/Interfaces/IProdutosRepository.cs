using API_Produtos.DAL.Entities;
using API_Produtos.DTO;

namespace API_Produtos.Repository.Interfaces
{
    public interface IProdutosRepository
    {
        bool CreateProducts(ProdutosDTO produtos);
        List<ProdutosDTO> GetAllProducts();
        Produto GetProduto(long id);
        string DeleteProduct(long id);
        bool PurchaseProduct(long ProdutoId, int qtde_comprada, string titular, string numero, string dataExp, string bandeira, string cvv);
    }
}
