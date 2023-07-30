using API_Produtos.DTO;

namespace API_Produtos.Manager.Interfaces
{
    public interface IProdutosValidate
    {
        bool Validate(ProdutosDTO produtos);
        List<ProdutosDTO> GetAll();
    }
}
