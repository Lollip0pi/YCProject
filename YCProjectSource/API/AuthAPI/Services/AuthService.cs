using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthAPI.Data;
using AuthAPI.Interfaces;
using AutoMapper;
using CommonModule.DTOs;
using CommonModule.Settings;
using CommonModule.Utilities;
using DTOsModule.DTOs.AuthAPI.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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
                // 檢核帳號申請是否已重複
                var checkUserAcct = await _AuthAPIDataContext.UserInfos.Where(m => m.UserAcct == rqObj.UserAcct && m.Status == "1").FirstOrDefaultAsync();
                if(checkUserAcct != null)
                {
                    throw new APISysErrorException(ApiStatusCode.ERR_USERID_EXISTED);
                }

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

        #region 登入
        /// <summary>
        /// 使用者登入
        /// </summary>
        /// <param name="rqObj"></param>
        /// <returns></returns>
        public async Task<LoginRs> UserLogin(LoginRq rqObj)
        {
            LoginRs result = new LoginRs();
            try
            {
                #region 驗證登入資訊

                result.StatusCode = ApiStatusCode.ERR_EXCEPTION;
                rqObj.UserPass = CryptyUtilities.Encrypt(rqObj.UserPass);
                //確認帳密是否正確
                var userInfo = await _AuthAPIDataContext.UserInfos.Where(m => m.UserAcct == rqObj.UserAcct
                    && m.UserPass == rqObj.UserPass && m.Status == "1").FirstOrDefaultAsync();
                if (userInfo != null)
                {
                    result.UserName = userInfo.UserName;
                }
                else
                {
                    throw new APISysErrorException(ApiStatusCode.ERR_AUTH_USERPASS_ERROR);
                }
                #endregion 驗證登入資訊

                #region 組登入票
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKKBytes = Encoding.UTF8.GetBytes(_authModuleSetting.Value.TokenKK);
                int TokenTimeout = _authModuleSetting.Value.TokenTimeout;

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                     new Claim("UserName", userInfo.UserName),
                     new Claim("UserGuid", userInfo.UserGuid.ToString()),
                     new Claim(ClaimTypes.Role, "VerifiedUser")
                    }),
                    Issuer = _authModuleSetting.Value.TokenIssuer,
                    Audience = _authModuleSetting.Value.TokenAudience,
                    Expires = DateTime.Now.AddSeconds(TokenTimeout),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(tokenKKBytes), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                result.AccessToken = tokenHandler.WriteToken(token);
                #endregion 組登入票

                #region 更新登入狀態
                var userLogin = await _AuthAPIDataContext.UserLogins.FindAsync(userInfo.UserGuid);
                if(userLogin !=null)
                {
                    userLogin.isLogin = "Y"; //是否登入 Y=是 N=否
                    userLogin.ModifyTime = DateTime.Now;

                    _AuthAPIDataContext.UserLogins.Update(userLogin);
                    await _AuthAPIDataContext.SaveChangesAsync();
                }
                else //從未登入過
                {
                    UserLogin newUserLogin = new UserLogin();
                    newUserLogin.UserGuid = userInfo.UserGuid;
                    newUserLogin.isLogin = "Y"; //是否登入 Y=是 N=否
                    newUserLogin.CreateTime = DateTime.Now;
                    newUserLogin.ModifyTime = DateTime.Now;

                    await _AuthAPIDataContext.UserLogins.AddAsync(newUserLogin);
                    await _AuthAPIDataContext.SaveChangesAsync();
                }
                #endregion 更新登入狀態

                #region 回傳資料
                result.StatusCode = ApiStatusCode.SUCCESS;
                #endregion 回傳資料

            }
            catch (APISysErrorException ASErr)
            {
                result.StatusCode = ASErr.StatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "登入失敗");
                result.StatusCode = ApiStatusCode.ERR_EXCEPTION;
            }
            return result;
        }
        #endregion 登入

        #region 註銷
        /// <summary>
        /// 使用者註銷
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        public async Task Revoked(string userGuid)
        {
            try
            {
                var userInfo = await _AuthAPIDataContext.UserInfos.FindAsync(Guid.Parse(userGuid));
                if(userInfo != null)
                {
                    #region 更新使用者資訊
                    userInfo.Status = "2"; //帳戶狀態1=有效 2=註銷
                    userInfo.ModifyTime = DateTime.Now;

                    _AuthAPIDataContext.UserInfos.Update(userInfo);
                    #endregion

                    #region 更新登入紀錄DB
                    var userLogin = await _AuthAPIDataContext.UserLogins.FindAsync(Guid.Parse(userGuid));
                    if(userLogin != null)
                    {
                        userLogin.isLogin = "N"; //是否登入 Y=是 N=否
                        userLogin.ModifyTime = DateTime.Now;

                        _AuthAPIDataContext.UserLogins.Update(userLogin);
                    }
                    #endregion
                    
                    await _AuthAPIDataContext.SaveChangesAsync();
                }
                else
                {
                    throw new APISysErrorException("使用者Guid不存在");
                }
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