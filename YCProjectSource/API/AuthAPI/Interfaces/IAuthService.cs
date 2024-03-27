using DTOsModule.DTOs.AuthAPI.Auth;

namespace AuthAPI.Interfaces
{
    public interface IAuthService
    {
        //註冊
        Task UserRegister(RegisterRq rqObj);

    }
}
