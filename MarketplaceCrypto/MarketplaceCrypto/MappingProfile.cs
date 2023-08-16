using AutoMapper;
using Entities.Models;
using Shared.DTO;

namespace CryptoMarketplace;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<ApplicationUser, GetUserDTO>().ReverseMap();
        CreateMap<ApplicationUser, UpdateUserDTO>().ReverseMap();
        CreateMap<ApplicationUser, RegisterUserDTO>().ReverseMap();


        CreateMap<CryptoCurrency,BinanceData>().ReverseMap();

        CreateMap<GetCryptocurrencyDTO, CryptoCurrency>().ReverseMap();
    }
}