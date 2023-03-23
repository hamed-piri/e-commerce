using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers;

public class MappingProfiles: Profile
{
    public MappingProfiles()
    {
        CreateMap<Product, ProductToReturnDto>()
            .ForMember(
                productToReturnDto => productToReturnDto.ProductType,
                expression => expression.MapFrom(product  => product.ProductType.Name)
            ).ForMember(
                productToReturnDto => productToReturnDto.ProductBrand,
                expression => expression.MapFrom(product  => product.ProductBrand.Name)
            ).ForMember(
                productToReturnDto => productToReturnDto.PictureUrl,
                expression => expression.MapFrom<ProductUrlResolver>()
            );
    }
}