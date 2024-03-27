using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using DTOsModule.DTOs.Common;
using Module.CommonModule.Interfaces;
using Module.CommonModule.DTOs;
using CommonModule.Extensions;
using CommonModule.DTOs;

namespace Module.CommonModule.Controllers
{
    /// <summary>
    /// Rest API 基礎類別, 所有的 rest 均以 api 開始
    /// 例如: QRPController, 則 request URI=api/qrp
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        public const string X_ClientID = "X-ClientID";
        public const string X_TXID = "X-TXID";
        public const string STATUS_CODE_SOURCE = "SYS";
        protected readonly ICommonService _commonService;

        public BaseApiController(ICommonService commonService)
        {
            _commonService = commonService;
        }

        /// <summary>
        /// 允許的 X_ClientID 類型
        /// </summary>
        public enum XClientIDType
        {
            ShopWeb,    //網頁
            ShopAPP     //APP
        }


        #region JsonResponse, 無泛型版本
        protected JsonResponse GetJsonResult(string code)
        {
            JsonResponse rt = new JsonResponse();
            rt.Status.Code = code;
            rt.Status.Desc = "";
            rt.Status.Desc = _commonService.GetStatusCodeMapping(STATUS_CODE_SOURCE, code, HeaderLanguage);
            if (!ModelState.IsValid && ApiStatusCode.ERR_DATA_VALIDATE == code)
                rt.Status.Errors = ModelState.AllErrors();
            return rt;
        }

        protected JsonResponse GetJsonResult(string code, string desc)
        {
            JsonResponse rt = new JsonResponse();
            rt.Status.Code = code;
            rt.Status.Desc = desc;
            return rt;
        }
        #endregion

        #region JsonResponse, 泛型版本
        protected JsonResponse<T> GetJsonResult<T>(string code)
        {
            JsonResponse<T> rt = new JsonResponse<T>();
            rt.Status.Code = code;
            rt.Status.Desc = _commonService.GetStatusCodeMapping(STATUS_CODE_SOURCE, code, HeaderLanguage);
            if (!ModelState.IsValid && ApiStatusCode.ERR_DATA_VALIDATE == code)
                rt.Status.Errors = ModelState.AllErrors();
            return rt;
        }

        protected JsonResponse<T> GetJsonResult<T>(string code, string desc)
        {
            JsonResponse<T> rt = new JsonResponse<T>();
            rt.Status.Code = code;
            rt.Status.Desc = desc;
            return rt;
        }

        protected JsonResponse<T> GetJsonResult<T>(T t, string code)
        {
            JsonResponse<T> rt = new JsonResponse<T>();
            rt.Status.Code = code;
            rt.Status.Desc = _commonService.GetStatusCodeMapping(STATUS_CODE_SOURCE, code, HeaderLanguage);
            if (!ModelState.IsValid && ApiStatusCode.ERR_DATA_VALIDATE == code)
                rt.Status.Errors = ModelState.AllErrors();
            rt.Body = t;
            return rt;
        }
        protected JsonResponse<T> GetJsonResult<T>(T t, string code, string desc)
        {
            JsonResponse<T> rt = new JsonResponse<T>();
            rt.Status.Code = code;
            rt.Status.Desc = desc;
            rt.Body = t;
            return rt;
        }
        #endregion

        #region 跟目前登入者資訊有關
        private string GetClaimTypeValue(string ClaimTypes)
        {
            ClaimsPrincipal user = (ClaimsPrincipal)HttpContext.User;
            string vv = user.Claims.Where(x => x.Type == ClaimTypes).Select(c => c.Value).SingleOrDefault();
            return vv + "";
        }

        protected string UserGuid => GetClaimTypeValue("UserGuid");
        protected string UserName => GetClaimTypeValue("UserName");
        protected bool IsAuthenticated => User != null ? User.Identity.IsAuthenticated : false;

        #endregion

        #region Header 共用取值 method, 供 rest api 取用
        protected string? HeaderXClientID => ControllerContext.HttpContext.Request.Headers[X_ClientID].FirstOrDefault();
        protected string? HeaderLanguage => ControllerContext.HttpContext.Request.AcceptLang();
        protected string? HeaderXTxID => ControllerContext.HttpContext.Request.Headers[X_TXID].FirstOrDefault();
        #endregion Header 共用取值 method, 供 rest api 取用

        /// <summary>
        /// Controller Action
        /// </summary>
        protected string HeaderActionName => ControllerContext.ActionDescriptor.ActionName;

        #region 取得登入票相關訊息類別
        protected BaseApiParam ApiHeadParam
        {
            get
            {
                return new BaseApiParam()
                {
                    HeaderXClientID = HeaderXClientID,
                    HeaderXTxID = HeaderXTxID,
                    HeaderLanguage = HeaderLanguage,
                    UserGuid = UserGuid,
                    UserName = UserName,
                    IsAuthenticated = IsAuthenticated,
                };
            }
        }
        #endregion


    }
}