using Microsoft.AspNetCore.Mvc;

namespace API_Produtos.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ProdutosController : ControllerBase
    {
        [HttpGet]
        public ActionResult HelthCheck()
        {
            return Ok("I'm alive and working");
        }
        
    }
}
