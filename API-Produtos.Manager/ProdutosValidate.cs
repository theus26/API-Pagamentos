using API_Produtos.DAL.Entities;
using API_Produtos.DTO;
using API_Produtos.Manager.Interfaces;
using API_Produtos.Repository.Interfaces;
using System.Text.RegularExpressions;

namespace API_Produtos.Manager
{
    public class ProdutosValidate : IProdutosValidate
    {
        private readonly IProdutosRepository _repository;
        public ProdutosValidate(IProdutosRepository repository)
        {
            _repository = repository;
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
        /// Iremos validar as informações repassadas pelo controller, caso esteja tudo certo irá chamar o repository para persistir os dados no BD.
        /// </summary>
        /// <param name="produtos"></param>
        /// <returns></returns>
        public bool Validate(ProdutosDTO produtos)
        {
            try
            {
                //Validação para ver se os campos são nulos ou vazios.
                if (string.IsNullOrEmpty(produtos.nome)) throw new ArgumentException("Produtos não podem ser nulos ou vazios.");
                if(produtos.valor_unitario == 0) throw new ArgumentException("Produtos não podem ter valor unitario como 0.");

                //Regex
                var Parser = new Regex(@"[a-zA-Z]+");
                var Matches = Parser.Matches(produtos.nome);
                if (Matches.Count == 0) throw new ArgumentException("Por favor insira somente letras");

                //Validação para o tamanho de cada campo
                if (produtos.nome.Length < 3) throw new ArgumentException("Nome do produto deve possuir mais de 3 caracteres.");

                //Chamar o repository para salvar os dados
                var salved = _repository.CreateProducts(produtos.nome, produtos.valor_unitario, produtos.qtde_estoque);

                if(salved == true)
                {
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
