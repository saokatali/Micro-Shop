using AutoMapper;
using Catalog.API.Common.Dto;
using Catalog.API.Domain.Models.Entities;

namespace Catalog.API.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<Category, CategoryDto>();

        }
    }
}
