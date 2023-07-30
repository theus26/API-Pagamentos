using API_Produtos.DTO;

namespace API_Produtos.Repository.Interfaces
{
    public interface IProdutosRepository
    {
        bool CreateProducts(string name, float valor_unitario, int qtde_estoque);
    }
}
