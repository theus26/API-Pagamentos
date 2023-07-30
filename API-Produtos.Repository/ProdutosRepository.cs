using API_Produtos.DAL.DAO;
using API_Produtos.DAL.Entities;
using API_Produtos.DTO;
using API_Produtos.Repository.Interfaces;

namespace API_Produtos.Repository
{
    public class ProdutosRepository : IProdutosRepository
    {
        private readonly IDAO<Produto> _produto;
       
        /// <summary>
        /// Criado injeção de dependencia para poder ter acesso as entidades no Banco de dados e poder gerencia e persistir os dados
        /// </summary>
        /// <param name="produto"></param>
        public ProdutosRepository(IDAO<Produto> produto)
        {
            _produto = produto;
        }
        #region Cadastrar Produtos
        /// <summary>
        /// Metodo responsavél por criar um produto no banco de dados
        /// instancia a classe para ela receber seus valores e salva-los no banco de dados
        /// </summary>
        /// <param name="name"></param>
        /// <param name="valor_unitario"></param>
        /// <param name="qtde_estoque"></param>
        /// <returns></returns>
        public bool CreateProducts(string name, float valor_unitario, int qtde_estoque)
        {
            try
            {
                 var newProduct = new Produto()
                 {
                     nome = name,
                     qtde_estoque = qtde_estoque,
                     valor_unitario = valor_unitario,
                 };

                 _produto.Create(newProduct);
                 return true;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Listar Produtos
        /// <summary>
        /// Metodo criado para consultar o banco de dados e retornar todos os produtos existente em forma de lista
        /// caso a busca seja nula, ele devolve uma exeção
        /// </summary>
        /// <returns></returns>
        public List<ProdutosDTO> GetAllProducts()
        {
            try
            {
                List<ProdutosDTO> produtosDTOs = new List<ProdutosDTO>();
                var getAll = _produto.GetAll().ToList();

                if (getAll != null)
                {
                    foreach (var item in getAll)
                    {
                        var newObj = new ProdutosDTO()
                        {
                            Id = item.IdProduto,
                            nome = item.nome,
                            qtde_estoque = item.qtde_estoque,
                            valor_unitario = item.valor_unitario,
                        };
                        
                        produtosDTOs.Add(newObj);
                    }
                      return produtosDTOs;
                }

                throw new Exception("Não foi possivel trazer os dados");
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
