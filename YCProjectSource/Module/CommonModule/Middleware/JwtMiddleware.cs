using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using CommonModule.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Module.CommonModule.DTOs;
using Module.CommonModule.Interfaces;

namespace CommonModule.Middlewares
{
    /// <summary>
    /// JWT Authentication 驗證 middleware
    /// </summary>
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IOptions<AuthModuleSetting> _authModuleSetting;
        private readonly ILogger<JwtMiddleware> _logger;
        // private readonly AADataContext _AADataContext;
        public JwtMiddleware(RequestDelegate next
        , IOptions<AuthModuleSetting> authModuleSetting
        , ILoggerFactory loggerFactory
        )
        {
            _next = next;
            _authModuleSetting = authModuleSetting;
            _logger = loggerFactory.CreateLogger<JwtMiddleware>();
        }

        public async Task Invoke(HttpContext context, IServiceProvider _IServiceProvider)
        {
            #region 遇AllowAnonymous跳過檢核 Authorization
            var endpoint = context.GetEndpoint();
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() is object)
            {
                if (token == null || (token != null &&
              (string.IsNullOrEmpty(token.ToString().Split(" ").Last()))
              || (string.IsNullOrEmpty(token.ToString().Replace("Bearer", "").Trim()))
              ))
                {
                    await _next(context);
                    return;
                }
            }
            #endregion
            bool goNext = true;
            if (token != null)
                goNext = await attachAccountToContext(context, token, _IServiceProvider);
            else
                goNext = await WriteMsg(context, _IServiceProvider, "AU059", "非合規之驗證票");

            _logger.LogInformation("JWT_End_");
            if (goNext)
                await _next(context);
            else
                return;

            //ClearNLogDiagnosticsContext();
        }

        private async Task<bool> attachAccountToContext(HttpContext context, string token, IServiceProvider _IServiceProvider)
        {
            try
            {
                _logger.LogInformation("_JWT_START_");
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_authModuleSetting.Value.TokenKK);
                ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _authModuleSetting.Value.TokenIssuer,
                    ValidateAudience = true,
                    ValidAudience = _authModuleSetting.Value.TokenAudience,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                context.User = claimsPrincipal;

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "JWT 驗證失敗");
                if (e.Message.Contains("IDX10223"))//時間逾期
                {
                    return await WriteMsg(context, _IServiceProvider, "AU057", "驗證票已逾期失效");
                }
                if (e.Message.Contains("IDX10214") || e.Message.Contains("IDX12741"))//token有值但不符合
                {
                    return await WriteMsg(context, _IServiceProvider, "AU059", "非合規之驗證票");
                }
                else
                    return true;
            }
        }
        #region 非正常驗證票 組回應訊息
        private async Task<bool> WriteMsg(HttpContext context, IServiceProvider _IServiceProvider
        , string statusCode, string statusDesc)
        {
            using (var scope = _IServiceProvider.CreateScope())
            {
                var _commonService = scope.ServiceProvider.GetRequiredService<ICommonService>();
                string acceptLang = context.Request.Headers["Accept-Language"].FirstOrDefault();
                acceptLang = string.IsNullOrEmpty(acceptLang) ? "zh-TW" : acceptLang;
                statusDesc = "非合規之驗證票";
                statusDesc = _commonService.MappingResource("WEBAPI", "SYS", statusCode, acceptLang, statusDesc);
                await WriteAuthFailedResponse(_commonService, context, statusCode, statusDesc);
                return false;
            }
        }
        #endregion
        #region 回送驗證票失敗，不再往下進行
        private async Task WriteAuthFailedResponse(ICommonService commonService, HttpContext httpContext, string statusCode, string statusDesc)
        {

            JsonResponse rt = new JsonResponse();
            rt.Status.Code = statusCode;
            rt.Status.Desc = statusDesc;
            httpContext.Response.StatusCode = (int)HttpStatusCode.OK;
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize<JsonResponse>(rt));

        }
        #endregion
    }
}