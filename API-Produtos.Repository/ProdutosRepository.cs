using API_Produtos.DAL.DAO;
using API_Produtos.DAL.Entities;
using API_Produtos.DTO;
using API_Produtos.Repository.Interfaces;
using API_Produtos.Utils.Requests.Interface;

namespace API_Produtos.Repository
{
    public class ProdutosRepository : IProdutosRepository
    {
        private readonly IDAO<Produto> _produto;
        private readonly IRequestPayament _util;
       
        /// <summary>
        /// Criado injeção de dependencia para poder ter acesso as entidades no Banco de dados e poder gerencia e persistir os dados
        /// </summary>
        /// <param name="produto"></param>
        public ProdutosRepository(IDAO<Produto> produto, IRequestPayament request )
        {
            _produto = produto;
            _util = request;
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
        /// <summary>
        /// Metodo criado deletar um produto do banco, caso encontrec o produto com o id especifico
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="NotImplementedException"></exception>
        public string DeleteProduct(long id)
        {
            // Verificar se o produto existe no banco de dados
            var getProduct = _produto.GetAll().FirstOrDefault(x => x.IdProduto == id);
            if (getProduct != null)
            {
                _produto.Delete(id);
                return $"Produto {id}, deletado com sucesso!";
            }
            throw new OperationCanceledException($"Não foi possível encontrar nenhum com o ID fornecido {id}");

        }

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

        #region Pegar produto pelo seu id
        /// <summary>
        /// Verificar se o produto existe no banco de dados apartir do id informado, caso não haja ele lança uma exceção
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Produto GetProduto(long id)
        {
            try
            {
                // Verificar se o produto existe no banco de dados
                var getProduct = _produto.GetAll().FirstOrDefault(x => x.IdProduto == id);

                if (getProduct != null)
                {
                    return getProduct;
                }

                throw new ArgumentException($"Não foi possivel encontrar produto com esse id: {id}");
            }
            catch
            {
                throw;
            }

        }
        #endregion
        /// <summary>
        /// Metodo criado para realizar a compra e o pagamento, ao receber os dados iremos verificar se o produto está disponivel, verificar a 
        /// quantidade do estoque, ter o valor final do produto comprado e por fim realizar a chamada da api.
        /// </summary>
        /// <param name="ProdutoId"></param>
        /// <param name="qtde_comprada"></param>
        /// <param name="titular"></param>
        /// <param name="numero"></param>
        /// <param name="dataExp"></param>
        /// <param name="bandeira"></param>
        /// <param name="cvv"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool PurchaseProduct(long ProdutoId, int qtde_comprada, string titular, string numero, string dataExp, string bandeira, string cvv)
        {
            try
            {
                var getProduct = _produto.GetAll().FirstOrDefault(x => x.IdProduto == ProdutoId);
                if (getProduct == null) throw new ArgumentException($"Produto em falta");

                if(getProduct.qtde_estoque == 0) throw new ArgumentException($"Produto Indisponivel");

                if (getProduct.qtde_estoque < qtde_comprada) throw new ArgumentException($"Quantidade de estoque indisponivel");

                float ValorTotal = getProduct.valor_unitario * qtde_comprada;

                //Intanciar Obj para fazer request
                InfoPayamentDTO infoPayament = new InfoPayamentDTO()
                {
                    valor = ValorTotal,
                    cartao = new Card()
                    {
                        bandeira = bandeira,
                        cvv = cvv,
                        data_expiracao = dataExp,
                        numero = numero,
                        titular = titular,

                    }
                };
                //Chamar a Request
                var request = _util.GetStateRequest(infoPayament).Result;

                if (request.Estado == "APROVADO")
                {
                    //Atualizar os dados do banco, adicionar data da venda e valor final
                    getProduct.DataUltimaVenda = DateTime.Now;
                    getProduct.ValorUltimaVenda = ((int)ValorTotal);
                    getProduct.qtde_estoque = getProduct.qtde_estoque - qtde_comprada;
                    _produto.Update(getProduct);
                    return true;

                }
                return false;

                
            }
            catch
            {
                throw;
            }



        }

    }
}
