using Microsoft.AspNetCore.Mvc;

using API_Produtos.DTO;
using API_Produtos.Manager.Interfaces;

namespace API_Produtos.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutosValidate _manager;
        public ProdutosController(IProdutosValidate manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public ActionResult HelthCheck()
        {
            return Ok("I'm alive and working");
        }

        [HttpPost]
        public IActionResult Produtos(ProdutosDTO produtos)
        {
            try
            {
                var validate = _manager.Validate(produtos);

                if(validate == true)
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
        
    }
}
