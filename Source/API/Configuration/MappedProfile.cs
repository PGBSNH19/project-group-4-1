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
                .ForMember(dest => dest.MarketplaceID, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<MarketplaceSeller, MarketplaceSellerDto>()
                .ForMember(dest => dest.MarketplaceID, opt => opt.Ignore())
                .ForMember(dest => dest.SellerID, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.ProductID, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<SellerPage, SellerPageDto>()
                .ForMember(dest => dest.SellerPageID, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<SellerPageProduct, SellerPageProductDto>()
                .ForMember(dest => dest.ProductID, opt => opt.Ignore())
                .ForMember(dest => dest.SellerPageID, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.UserID, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<UserProduct, UserProductDto>()
                .ForMember(dest => dest.UserID, opt => opt.Ignore())
                .ForMember(dest => dest.ProductID, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
