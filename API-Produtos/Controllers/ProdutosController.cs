using Microsoft.AspNetCore.Mvc;

using API_Produtos.DTO;
using API_Produtos.Manager.Interfaces;

namespace API_Produtos.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ProdutosController : ControllerBase
    {
        /// <summary>
        /// Adicionando dependencia do Manager ao controler do projeto para que possa se utilizar todos os metodos disponivéis criados lá.
        /// </summary>
        private readonly IProdutosValidate _manager;
        public ProdutosController(IProdutosValidate manager)
        {
            _manager = manager;
        }

        /// <summary>
        /// Metodo criado para testar a vida e funcionamento da API, antes de realizar as chamadas principais 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult HelthCheck()
        {
            return Ok("I'm alive and working");
        }

        /// <summary>
        /// Endpoint criado para realizar o cadastramento do produto, aqui é chamado o manager para realizar as validações dos dados informados
        /// e caso tudo ocorra bem, ele retorna 200, caso haja algum erro na validação do usuario, devolve 412 e caso aconteça algum erro desconhecido
        /// retora 400
        /// </summary>
        /// <param name="produtos"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Produtos(ProdutosDTO produtos)
        {
            try
            {
                var validate = _manager.Validate(produtos);

                if(validate)
                {
                    return Ok("Produto Cadastrado");
                }

                return BadRequest("Ocorreu um erro desconhecido");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status412PreconditionFailed, new ErrorResponseDTO()
                {
                    status = StatusCodes.Status412PreconditionFailed,
                    message = "Os valores informados não são válidos",
                    info = ex.Message

                });
            }
        }

        /// <summary>
        /// Endpoint criado para retornar uma lista de produtos cadastrados
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            try
            {
                var getProducts = _manager.GetAll();
                return Ok(getProducts);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ErrorResponseDTO()
                {
                    status = StatusCodes.Status400BadRequest,
                    message = "Ocorreu um erro desconhecido",
                    info = ex.Message

                });
            }
        }

        /// <summary>
        /// Endpoint criado para detalhar um produto pelo seu Id, nesse caso é chamado o manager para validar o id, caso ocorra tudo bem, ele irá devolver
        /// 200, contendo o produto especificado, caso contrario ele devolverá 400.
        /// </summary>
        /// <param name="IdProduto"></param>
        /// <returns></returns>
        [HttpGet("{IdProduto}")]
        public IActionResult GetProductId(long IdProduto)
        {
            try
            {
                var getProduct = _manager.GetProductId(IdProduto);
                return Ok(getProduct);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ErrorResponseDTO()
                {
                    status = StatusCodes.Status400BadRequest,
                    message = "Ocorreu um erro desconhecido",
                    info = ex.Message

                });
            }
        }

        /// <summary>
        /// Endpoint criado para apagar um registro da base de dados pelo seu Id, caso ele encontre ele retornará 200, com uma mensagem informando que 
        /// o produto foi deletado, caso não devolverá 400, informando que não foi possivel realizar operação.
        /// </summary>
        /// <param name="ProdutoId"></param>
        /// <returns></returns>
        [HttpDelete("{ProdutoId}")]
        public IActionResult DeleteProduct(long ProdutoId)
        {
            try
            {
                _manager.DeleteProduct(ProdutoId);
                return Ok("Produto excluído com sucesso");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new ErrorResponseDTO()
                {
                    status = StatusCodes.Status400BadRequest,
                    message = "Ocorreu um erro desconhecido",
                    info = ex.Message

                });
            }
        }

        /// <summary>
        /// Enpoint criado para realizar a venda do produto, recebe os dados do usuario e repassar para o manager realizar a validação de cada dado
        /// caso ocorra tudo bem, retornará 200, informando que a venda foi realizada com sucesso.
        /// caso ocorra erros nas validações ele irá retornar 412 e caso ocorra alguma falha desconhecida retornará 400.
        /// </summary>
        /// <param name="purchaseProducts"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult PurchaseProduct(PurchaseProductsDTO purchaseProducts)
        {
            try
            {
                var purchase = _manager.PurchaseProduct(purchaseProducts);

                if (purchase)
                {
                    return Ok("Venda realizada com sucesso");
                }

                return BadRequest("Ocorreu um erro desconhecido");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status412PreconditionFailed, new ErrorResponseDTO()
                {
                    status = StatusCodes.Status412PreconditionFailed,
                    message = "Os valores informados não são válidos",
                    info = ex.Message

                });
            }
        }

    }
}
