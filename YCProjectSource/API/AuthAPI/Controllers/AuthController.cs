
using AuthAPI.Interfaces;
using CommonModule.DTOs;
using DTOsModule.DTOs.AuthAPI.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Module.CommonModule.Controllers;
using Module.CommonModule.DTOs;
using Module.CommonModule.Exceptions;
using Module.CommonModule.Interfaces;

namespace AuthAPI.Controllers
{
    /// <summary>
    /// 認證授權類 API
    /// </summary>
    [Authorize]
    public class AuthController : BaseApiController
    {
        private const string ControllerTXID = "Auth";
        private const string ControllerTXNAME = "登入/驗證";
        private readonly ILogger<AuthController> _logger;
        private IAuthService _IAuthService;
        public AuthController(IAuthService IAuthService
            , ICommonService commonService
            , ILogger<AuthController> logger
        ) : base(commonService)
        {
            _IAuthService = IAuthService;
            _logger = logger;
        }

        #region Register, 首頁註冊
        /// <summary>
        /// 首頁註冊<br/>
        /// </summary>
        /// <remarks>
        /// ==========================================================================<br/>
        /// {
        ///     "UserName": "Randy",
        ///     "UserAcct": "test0101",
        ///     "UserPass": "test0202"
        /// }
        /// ==========================================================================<br/>
        /// </remarks>
        /// <param name="rqObj"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<JsonResponse> Register([Bind] RegisterRq rqObj)
        {
            string statusCode = ApiStatusCode.ERR_EXCEPTION;
            string statusDesc = "";
            string prcDt = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
            try
            {
                if (!ModelState.IsValid)
                {
                    statusCode = ApiStatusCode.ERR_DATA_VALIDATE;
                    return GetJsonResult(ApiStatusCode.ERR_DATA_VALIDATE);
                }

                await _IAuthService.UserRegister(rqObj);
                statusCode = ApiStatusCode.SUCCESS;

                return GetJsonResult(statusCode);
            }
            catch (APISysErrorException ASErr)
            {
                return GetJsonResult(ASErr.StatusCode);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Register Error");
                statusDesc = e.Message;
                return GetJsonResult(ApiStatusCode.ERR_EXCEPTION);
            }
            finally
            {
                // 寫 TransactionLog
            }

        }
        #endregion

    }
}