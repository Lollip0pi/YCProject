using DTOsModule.DTOs.AuthAPI.Auth;

namespace AuthAPI.Interfaces
{
    public interface IAuthService
    {
        //登入
        Task<LoginRs> UserLogin(LoginRq rqObj);

        //註冊
        Task UserRegister(RegisterRq rqObj);

    }
}
