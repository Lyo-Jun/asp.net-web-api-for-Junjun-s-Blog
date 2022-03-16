using AutoMapper;
using WebApplication3.Dtos;
using WebApplication3.Entities;

namespace WebApplication3.Helper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<ArticleDto, Article>().ReverseMap();
        CreateMap<Tag, TagDto>().ReverseMap();
        CreateMap<Category, CategoryDto>().ReverseMap();
    }
}