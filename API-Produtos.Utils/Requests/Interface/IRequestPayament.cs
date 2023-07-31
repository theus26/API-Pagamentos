using API_Produtos.DTO;

namespace API_Produtos.Utils.Requests.Interface
{
    public interface IRequestPayament
    {
        Task<ResponseRequestDTO> GetStateRequest(InfoPayamentDTO infoPayament);
    }
}
