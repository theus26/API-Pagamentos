using API_Produtos.DAL.Entities;
using API_Produtos.DTO;
using AutoMapper;

namespace API_Produtos.Utils.IMapper
{
    public class Mappers : Profile
    {
        public Mappers()
        {
            CreateMap<Produto, ProdutosDTO>().ReverseMap();
        }

    }
}
