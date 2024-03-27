
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

        #region Register, 註冊
        /// <summary>
        /// 註冊<br/>
        /// </summary>
        /// <remarks>
        /// ==========================================================================<br/>
        /// {
        ///     "UserName": "Test",
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

        #region Login, 登入
        /// <summary>
        /// 登入<br/>
        /// </summary>
        /// <remarks>
        /// ==========================================================================<br/>
        /// ==========================================================================<br/>
        /// </remarks>
        /// <param name="rqObj"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<JsonResponse<LoginRs>> Login([Bind] LoginRq rqObj)
        {
            string statusCode = ApiStatusCode.ERR_EXCEPTION;
            string statusDesc = "";
            string prcDt = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
            LoginRs rsObj = new LoginRs();
            try
            {

                await checkLoginValid(rqObj);
                if (!ModelState.IsValid)
                {
                    statusCode = ApiStatusCode.ERR_DATA_VALIDATE;
                    return GetJsonResult<LoginRs>(ApiStatusCode.ERR_DATA_VALIDATE);
                }

                rsObj = await _IAuthService.UserLogin(rqObj);
                if (rsObj != null && rsObj.StatusCode != "0")
                {
                    return GetJsonResult<LoginRs>(rsObj.StatusCode);
                }
                statusCode = ApiStatusCode.SUCCESS;

                return GetJsonResult(rsObj, statusCode);
            }
            catch (APISysErrorException ASErr)
            {
                return GetJsonResult<LoginRs>(ASErr.StatusCode);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Login Error");
                statusDesc = e.Message;
                return GetJsonResult<LoginRs>(ApiStatusCode.ERR_EXCEPTION);
            }
            finally
            {
                // 寫 TransactionLog
            }

        }
        #endregion

        #region 檢核
        private async Task checkLoginValid(LoginRq rqObj)
        {
            if (rqObj.UserAcct.Length < 8)
            {
                ModelState.AddModelError(rqObj.UserAcct, "帳號長度需大於等於8");
            }
            if (rqObj.UserPass.Length < 8)
            {
                ModelState.AddModelError(rqObj.UserPass, "密碼長度需大於等於8");
            }
        }
        #endregion 檢核

    }
}