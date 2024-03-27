using AuthAPI.Data;
using AuthAPI.Interfaces;
using AutoMapper;
using CommonModule.DTOs;
using CommonModule.Settings;
using CommonModule.Utilities;
using DTOsModule.DTOs.AuthAPI.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Module.CommonModule.Exceptions;
using Module.CommonModule.Interfaces;
using SqlEntitiesModule.Entities.YCDataBase.Table;

namespace AAAPI.Services
{
    /// <summary>
    /// 使用者登入/註冊服務
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly IOptions<AuthModuleSetting> _authModuleSetting;
        private readonly ICommonService _CommonService;
        private readonly AuthAPIDataContext _AuthAPIDataContext;
        private readonly IMapper _Mapper;
        private ILogger<AuthService> _logger;

        public AuthService(IOptions<AuthModuleSetting> authModuleSetting
        , ICommonService CommonService
        , AuthAPIDataContext AuthAPIDataContext
        , IMapper Mapper
        , ILogger<AuthService> logger)
        {
            _authModuleSetting = authModuleSetting;
            _CommonService = CommonService;
            _AuthAPIDataContext = AuthAPIDataContext;
            _Mapper = Mapper;
            _logger = logger;
        }


        #region 註冊
        /// <summary>
        /// 使用者註冊
        /// </summary>
        /// <param name="rqObj"></param>
        /// <returns></returns>
        public async Task UserRegister(RegisterRq rqObj)
        {
            try
            {
                UserInfo userData = new UserInfo();
                var insertData = _Mapper.Map<RegisterRq, UserInfo>(rqObj);
                insertData.UserGuid = Guid.NewGuid();
                insertData.UserPass = CryptyUtilities.Encrypt(rqObj.UserPass);
                insertData.Status = "1";//1=啟用 2=停用
                insertData.CreateTime = DateTime.Now;
                insertData.ModifyTime = DateTime.Now;

                // 儲存至資料庫
                await _AuthAPIDataContext.UserInfos.AddAsync(insertData);
                await _AuthAPIDataContext.SaveChangesAsync();

            }
            catch (APISysErrorException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "註冊失敗");
                throw;
            }
        }
        #endregion 註冊

    }

}