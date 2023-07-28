using AutoMapper;
using ItemService.Dtos;
using ItemService.Models;

namespace ItemService.Profiles
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<Restaurante, RestauranteReadDto>();
            CreateMap<RestauranteReadDto, Restaurante>()
                .ForMember(d => d.IdExterno, o => o.MapFrom(src => src.Id));
            CreateMap<ItemCreateDto, Item>();
            CreateMap<Item, ItemCreateDto>();
        }
    }
}