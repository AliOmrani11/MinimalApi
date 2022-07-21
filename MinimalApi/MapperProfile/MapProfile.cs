using AutoMapper;
using MinimalApi.Domain;
using MinimalApi.Dto;

namespace MinimalApi.MapperProfile;

public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<Book, BookDto>().ReverseMap();
        CreateMap<Book, GetBookDto>().ReverseMap();
    }
}