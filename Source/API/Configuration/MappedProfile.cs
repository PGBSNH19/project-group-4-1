using API.Dtos;
using API.Models;
using AutoMapper;

namespace API.Configuration
{
    public class MappedProfile : Profile
    {
        public MappedProfile()
        {
            CreateMap<Marketplace, MarketplaceDto>()
                .ReverseMap();
            CreateMap<MarketplaceSeller, MarketplaceSellerDto>()
                .ReverseMap();
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.ProductID, opt => opt.MapFrom(src => src.ProductID))
                .ReverseMap();
            CreateMap<SellerPage, SellerPageDto>()
                .ForMember(dest => dest.SellerPageID, opt => opt.MapFrom(src => src.SellerPageID))
                .ReverseMap();
            CreateMap<SellerPageProduct, SellerPageProductDto>()
                .ForMember(dest => dest.SellerPageID, opt => opt.MapFrom(src => src.SellerPageID))
                .ForMember(dest => dest.ProductID, opt => opt.MapFrom(src => src.ProductID))
                    .ReverseMap();
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.UserID))
                .ReverseMap();
            CreateMap<UserProduct, UserProductDto>()
                .ReverseMap();
            CreateMap<Product, ProductPutDto>()
                .ForMember(dest => dest.ProductID, opt => opt.MapFrom(src => src.ProductID))
                .ReverseMap();
        }
    }
}
