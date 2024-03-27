using System.Text.Json;
using Microsoft.Extensions.Options;
using System.Globalization;
using Module.CommonModule.Interfaces;
using Module.CommonModule.Controllers;
using CommonModule.DTOs;
using static Module.CommonModule.Controllers.BaseApiController;
using Module.CommonModule.DTOs;
using CommonModule.Extensions;

namespace CommonModule.Middleware
{
    /// <summary>
    /// 共用 Header 攔截器, 如 Http Header 未帶這些欄位或值不正確, 則回覆失敗 JsonResponse, 如欲略過檢核, 可設白名單
    /// "CommonHeader" : {
    ///    "AllowURIs": ["/api/auth/login"]
    /// } 
    /// </summary>
    public class CommHeaderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CommHeaderMiddleware> _logger;

        public CommHeaderMiddleware(RequestDelegate next, ILogger<CommHeaderMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// 因 Middleware 屬 Singleton, 不可以在 c'tor DI Scoped 服務, 所以改在方法做注入 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="commonService"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context, ICommonService commonService)
        {
            string defaultLang = "zh-TW";
            string acceptLang = context.Request.Headers["Accept-Language"].FirstOrDefault();
            acceptLang = string.IsNullOrEmpty(acceptLang) ? defaultLang : acceptLang.Split(",")[0];

            CultureInfo newCulture = CreatCultureInfo(acceptLang, defaultLang); //new CultureInfo(acceptLang);
            Thread.CurrentThread.CurrentCulture = newCulture;
            Thread.CurrentThread.CurrentUICulture = newCulture;

            #region X-TXID 必要檢核
            var requestXTxId = context.Request.Headers[BaseApiController.X_TXID].FirstOrDefault();
            if (requestXTxId == null)
            {
                await WriteMissingRequiredHeaderResponse(commonService, context, ApiStatusCode.ERR_HEADER_VALIDATE, BaseApiController.X_TXID);
                return;
            }
            #endregion

            #region X-ClientID 必要及是否為值檢核
            var requestClientID = context.Request.Headers[BaseApiController.X_ClientID].FirstOrDefault();
            if (requestClientID == null)
            {
                await WriteMissingRequiredHeaderResponse(commonService, context, ApiStatusCode.ERR_HEADER_VALIDATE, BaseApiController.X_ClientID);

                return;
            }

            bool xClientIDIsValid = false;
            if (string.Compare(requestClientID, Enum.GetName(typeof(XClientIDType), XClientIDType.ShopWeb), true) == 0)
                xClientIDIsValid = true;

            if (xClientIDIsValid)
            {
                await _next(context);
                return;
            }
            else
            {
                await WriteMissingRequiredHeaderResponse(commonService, context, ApiStatusCode.ERR_HEADER_VALIDATE, BaseApiController.X_ClientID);
                return;
            }
            #endregion
        }

        private async Task WriteMissingRequiredHeaderResponse(ICommonService commonService, HttpContext httpContext, string statusCode, string headerName)
        {
            JsonResponse rt = new JsonResponse();
            rt.Status.Code = statusCode;
            rt.Status.Desc = commonService.GetStatusCodeMapping(BaseApiController.STATUS_CODE_SOURCE, statusCode, httpContext.Request.AcceptLang());
            rt.Status.Desc += $"[{headerName}]";

            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize<JsonResponse>(rt));

        }

        private CultureInfo CreatCultureInfo(string acceptLang, string defaultLang)
        {
            CultureInfo newCulture = null;
            try
            {
                newCulture = new CultureInfo(acceptLang);
            }
            catch (Exception e)
            {
                _logger.LogInformation(e.StackTrace);
            }

            if (newCulture == null)
            {
                newCulture = new CultureInfo(defaultLang);
            }

            return newCulture;
        }
    }
}