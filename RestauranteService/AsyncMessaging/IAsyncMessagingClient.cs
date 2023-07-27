using RestauranteService.Dtos;

namespace RestauranteService.AsyncMessaging
{
    public interface IAsyncMessagingClient
    {
        void PublicarRestaurante(RestauranteReadDto restauranteReadDto);
    }
}
