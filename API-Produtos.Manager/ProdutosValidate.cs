using API_Produtos.DAL.Entities;
using API_Produtos.DTO;
using API_Produtos.Manager.Interfaces;
using API_Produtos.Manager.Validate;
using API_Produtos.Repository.Interfaces;
using FluentValidation.Results;
using System.Text.RegularExpressions;

namespace API_Produtos.Manager
{
    public class ProdutosValidate : IProdutosValidate
    {
        /// <summary>
        /// Adicionando injeção de dependencia para que o Manager tem acesso aos metodos do repository.
        /// </summary>
        private readonly IProdutosRepository _repository;
        public ProdutosValidate(IProdutosRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Metodo responsavel por deletar o produto, esse metodo recebe o Id, valida para verificar se é um positivo inteiro, caso seja
        /// ele chamar o repository.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DeleteProduct(long id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new ArgumentException("O ID do produto deve ser um número inteiro positivo.");
                }
                 _repository.DeleteProduct(id);
                return $"Produto {id}, deletado com sucesso!";
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Método criado para devolver todos os produtos
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<ProdutosDTO> GetAll()
        {
            try
            {
                var getProducts = _repository.GetAllProducts();
                return getProducts;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Chama o repository para detalhar um produto pelo Id especificado, e valida para verificar se o Id é um inteiro positivo, caso não seja
        /// lança uma exeção.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Produto GetProductId(long id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new ArgumentException("O ID do produto deve ser um número inteiro positivo.");
                }
                var getProductId = _repository.GetProduto(id);

                return getProductId;
            }

            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Metodo responsavél por validar as informações que serão usadas para requisitar o gateway de pagamento
        /// </summary>
        /// <param name="productsDTO"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool PurchaseProduct(PurchaseProductsDTO productsDTO)
        {
            try
            {
                var validator = new PurchaseValidate();
                ValidationResult result = validator.Validate(productsDTO);

                if (!result.IsValid)
                {
                    foreach (var failure in result.Errors)
                    {
                        throw new Exception($"Erro: {failure.ErrorMessage}");
                    }
                }
                //Chamar o repository
                var getRepository = _repository.PurchaseProduct(productsDTO.produto_id, productsDTO.qtde_comprada, productsDTO.cartao.titular, productsDTO.cartao.numero, productsDTO.cartao.data_expiracao, productsDTO.cartao.bandeira, productsDTO.cartao.cvv);

                return getRepository;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Iremos validar as informações repassadas pelo controller, caso esteja tudo certo irá chamar o repository para persistir os dados no BD.
        /// </summary>
        /// <param name="produtos"></param>
        /// <returns></returns>
        public bool Validate(ProdutosDTO produtos)
        {
            try
            {
                
                var validate = new ManageValidate();
                ValidationResult result = validate.Validate(produtos);
                if (!result.IsValid)
                {
                    foreach (var failure in result.Errors)
                    {
                        throw new Exception($"Propriedade: {failure.PropertyName}, Erro: {failure.ErrorMessage}");
                    }
                }

                //Chamar o repository para salvar os dados
                var salved = _repository.CreateProducts(produtos);

                return salved;
            }
            catch
            {
                throw;
            }
        }
    }
}
