using API_Produtos.DAL.Entities;
using API_Produtos.DTO;

namespace API_Produtos.Repository.Interfaces
{
    public interface IProdutosRepository
    {
        bool CreateProducts(string name, float valor_unitario, int qtde_estoque);
        List<ProdutosDTO> GetAllProducts();
        Produto GetProduto(long id);
        string DeleteProduct(long id);
    }
}
