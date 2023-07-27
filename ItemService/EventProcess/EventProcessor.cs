using AutoMapper;
using ItemService.Data;
using ItemService.Dtos;
using ItemService.Models;
using System.Text.Json;

namespace ItemService.EventProcess
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IMapper _mapper;
        private readonly IServiceScopeFactory _scopeFactory;

        public EventProcessor(IMapper mapper, IServiceScopeFactory scopeFactory)
        {
            _mapper = mapper;
            _scopeFactory = scopeFactory;
        }

        public void ProcessarEventoRestaurante(string mensagem)
        {
            using var scope = _scopeFactory.CreateScope();

            var itemRepository = scope.ServiceProvider.GetRequiredService<IItemRepository>();

            var restauranteReadDto = JsonSerializer.Deserialize<RestauranteReadDto>(mensagem);

            var restaurante = _mapper.Map<Restaurante>(restauranteReadDto);

            if(!itemRepository.ExisteRestauranteExterno(restaurante.Id))
            {
                itemRepository.CreateRestaurante(restaurante);
                itemRepository.SaveChanges();
            }
        }
    }
}
