﻿using AutoMapper;
using Karim.ECommerce.Application.Abstraction.Dtos.Products;
using Karim.ECommerce.Domain.Entities.Carts;
using Karim.ECommerce.Domain.Entities.Products;
using Karim.ECommerce.Shared.Dtos.Carts;
using Karim.ECommerce.Shared.Dtos.Products;

namespace Karim.ECommerce.Application.Mapper
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Product
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand!.BrandName))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category!.CategoryName))
                .ForMember( dest => dest.MainImage, opt => opt.MapFrom<ImageResolver<Product,ProductToReturnDto>>() )
                .ForMember( dest => dest.ImagesCollection, opt => opt.MapFrom<ProductImageCollectionResolver>() );

            //Brand
            CreateMap<Brand, BrandToReturnDto>()
                .ForMember(dest => dest.MainImage, opt => opt.MapFrom<ImageResolver<Brand, BrandToReturnDto>>())
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories!.Select(e => e.Category)));

            CreateMap<Brand, BrandInCategoryDto>();

            //Category
            CreateMap<Category, CategoryToReturnDto>()
                .ForMember(dest => dest.MainImage, opt => opt.MapFrom<ImageResolver<Category, CategoryToReturnDto>>())
                .ForMember(dest => dest.Brands, opt => opt.MapFrom(src => src.Brands!.Select(e => e.Brand)));

            CreateMap<Category, CategoryInBrandDto>();


            //Cart
            CreateMap<CartItem, CartItemDto>().ReverseMap();
            CreateMap<Cart, CartToReturnDto>().ReverseMap();
        }
    }
}
