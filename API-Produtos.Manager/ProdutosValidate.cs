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

        public string DeleteProduct(long id)
        {
            try
            {
                // Validar se o ID é um número inteiro positivo
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
        /// Chama o repository para realizar a chamada no banco de dados caso exista retorna o produto expecificado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Produto GetProductId(long id)
        {
            try
            {
                // Validar se o ID é um número inteiro positivo
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
                //Validação para ver se os campos são nulos ou vazios.
                if(productsDTO.produto_id  <= 0 || productsDTO.qtde_comprada <= 0) throw new ArgumentException("Valores informados invalidos.");
                if (string.IsNullOrEmpty(productsDTO.cartao.titular)) throw new ArgumentException("Campos para realizar a compra não podem ser nulos ou vazios.");
                if (string.IsNullOrEmpty(productsDTO.cartao.bandeira)) throw new ArgumentException("Campos para realizar a compra não podem ser nulos ou vazios.");
                if (string.IsNullOrEmpty(productsDTO.cartao.data_expiracao)) throw new ArgumentException("Campos para realizar a compra não podem ser nulos ou vazios.");
                if (string.IsNullOrEmpty(productsDTO.cartao.numero)) throw new ArgumentException("Campos para realizar a compra não podem ser nulos ou vazios.");
                if (string.IsNullOrEmpty(productsDTO.cartao.cvv)) throw new ArgumentException("Campos para realizar a compra não podem ser nulos ou vazios.");

                //Validação para o tamanho de cada campo
                if (productsDTO.cartao.bandeira.Length < 3) throw new ArgumentException("O campo Bandeira deve possuir mais de 3 caracteres.");
                if (productsDTO.cartao.cvv.Length < 3) throw new ArgumentException("O campo CVV deve possuir mais de 3 caracteres.");
                if (productsDTO.cartao.data_expiracao.Length < 3) throw new ArgumentException("A Data de Expiração deve possuir mais de 3 caracteres.");
                if (productsDTO.cartao.numero.Length < 3) throw new ArgumentException("O campo Numero deve possuir mais de 3 caracteres.");
                if (productsDTO.cartao.titular.Length < 3) throw new ArgumentException("O campo Titular deve possuir mais de 3 caracteres.");

                // Regex
                
                // Remover espaços em branco, caso existam
                var NumberCard = Regex.Replace(productsDTO.cartao.numero, @"\s", "");

                //Cartão
                var parser = new Regex(@"^\d{16}$");
                var Matches = parser.Matches(NumberCard);
                if (Matches.Count == 0) throw new ArgumentException("Cartão Inválido");

                //Data de expiração
                var ParserDate = new Regex(@"^(0[1-9]|1[0-2])\/\d{2}$");
                var MatchesDate = ParserDate.Matches(productsDTO.cartao.data_expiracao);
                if (MatchesDate.Count == 0) throw new ArgumentException("Data de Expiração Inválida");

                //Nome do titular
                var ParserName = new Regex(@"[a-zA-Z]+");
                var MatchesName = ParserName.Matches(productsDTO.cartao.titular);
                if (MatchesName.Count == 0) throw new ArgumentException("Nome do Titular Inválido");

                //Bandeira
                var ParserBand = new Regex(@"[a-zA-Z]+");
                var MatchesBand = ParserBand.Matches(productsDTO.cartao.bandeira);
                if (MatchesBand.Count == 0) throw new ArgumentException("Bandeira do Cartão Inválida");

                //Chamar o repository
                var getRepository = _repository.PurchaseProduct(productsDTO.produto_id, productsDTO.qtde_comprada, productsDTO.cartao.titular, NumberCard, productsDTO.cartao.data_expiracao, productsDTO.cartao.bandeira, productsDTO.cartao.cvv);

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
