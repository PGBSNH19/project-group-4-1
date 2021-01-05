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
                .ReverseMap();
            CreateMap<SellerPage, SellerPageDto>()
                .ReverseMap();
            CreateMap<SellerPageProduct, SellerPageProductDto>()
                .ReverseMap();
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.UserID, opt => opt.MapFrom(src => src.UserID))
                 .ReverseMap();
        }
    }
}
