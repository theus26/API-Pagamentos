using API_Produtos.DTO;
using API_Produtos.Utils.Requests.Interface;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Text;

namespace API_Produtos.Utils.Requests
{
    public class RequestPayament : IRequestPayament
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        public RequestPayament(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseRequestDTO> GetStateRequest(InfoPayamentDTO infoPayament)
        {
            var url = _configuration.GetSection("Urls").GetSection("ApiPayament").Value;
            var deserialize = new ResponseRequestDTO();

            //Iniciando a request
            var client = _httpClientFactory.CreateClient();
            try
            {
                // Serializa o objeto para formato JSON
                var requestBodyJson = JsonConvert.SerializeObject(infoPayament);

                // Cria o conteúdo da requisição com o corpo (body) em formato JSON
                var httpContent = new StringContent(requestBodyJson, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, httpContent);
                

                var status = response.IsSuccessStatusCode; // Verifica se a requisição foi bem-sucedida

                if (status == true)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    try
                    {
                        deserialize = JsonConvert.DeserializeObject<ResponseRequestDTO>(content);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }

                else
                {
                    throw new HttpRequestException($"Não foi possivel realizar a request");
                }

                
                return deserialize;
            }
            catch
            {
                throw;
            }
        }
    }
}
