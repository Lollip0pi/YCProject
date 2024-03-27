using AutoMapper;
using DTOsModule.DTOs.AuthAPI.Auth;
using SqlEntitiesModule.Entities.YCDataBase.Table;

namespace AuthAPI.Profiles
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<RegisterRq, UserInfo>();
        }
    }
}