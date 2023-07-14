using RestauranteService.Dtos;
using System.Text.Json;
using System.Text;

namespace RestauranteService.Http
{
    public class ItemServiceHttpClient : IItemServiceHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public ItemServiceHttpClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async void EnviaRestauranteParaItemService(RestauranteReadDto readDto)
        {
            var conteudoHttp = new StringContent(
                    JsonSerializer.Serialize(readDto),
                    Encoding.UTF8,
                    "application/json"
                );

            await _httpClient.PostAsync(_configuration["ItemService"], conteudoHttp);
        }
    }
}
