using AutoMapper;
using PayCorona.Dto;
using PayCorona.Models;

namespace PayCorona.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Client, ClientDto>();
            CreateMap<Wallet, WalletDto>();
        }
    }
}
