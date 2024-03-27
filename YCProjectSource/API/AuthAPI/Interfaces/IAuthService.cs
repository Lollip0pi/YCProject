using DTOsModule.DTOs.AuthAPI.Auth;
using DTOsModule.DTOs.Common;

namespace AuthAPI.Interfaces
{
    public interface IAuthService
    {
        //登入
        Task<LoginRs> UserLogin(LoginRq rqObj);

        //註冊
        Task UserRegister(RegisterRq rqObj);

        //註銷
        Task Revoked(string userGuid);

    }
}
