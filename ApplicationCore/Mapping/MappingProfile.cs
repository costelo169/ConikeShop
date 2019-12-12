using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using AutoMapper;
namespace ApplicationCore.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<SaveProductDto, Product>();
            CreateMap<ProductDto, SaveProductDto>();
        }
    }
}