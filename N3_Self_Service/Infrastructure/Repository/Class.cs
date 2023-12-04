
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using N3_Self_Service.Domain.Model;
    using Newtonsoft.Json;
namespace N3_Self_Service.Infrastructure.Repository
{

    public class MinhaClasseDeCliente
    {
        private readonly HttpClient _httpClient;

        public MinhaClasseDeCliente(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task EnviarRequisicaoComTokenAsync(string token, ProdutoModel produto)
        {
            // Adiciona o token ao cabeçalho de autorização
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Converte o objeto produto em um JSON e envia a requisição
            var produtoJson = JsonConvert.SerializeObject(produto);
            var content = new StringContent(produtoJson, Encoding.UTF8, "application/json");

            // Chama a API
            var response = await _httpClient.PostAsync("https://localhost:7134/swagger/registrar-produto", content);

            // Verifica a resposta
            if (response.IsSuccessStatusCode)
            {
                
            }
            else
            {
               
            }
        }
    }

}
