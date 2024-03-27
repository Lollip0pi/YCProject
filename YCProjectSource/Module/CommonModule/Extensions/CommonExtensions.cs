using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Text.Json;
using CommonModule.DTOs;
using Microsoft.AspNetCore.Diagnostics;
using Module.CommonModule.DTOs;

namespace CommonModule.Extensions
{
    /// <summary>
    /// 共用類擴充方法
    /// </summary>
    public static class CommonExtensions
    {
        /// <summary>
        /// 判斷 IConfiguration, key 是否存在
        /// </summary>
        /// <param name="config"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool KeyExist(this IConfiguration config, string key)
        {
            return config.GetChildren().Any(item => item.Key == key);
        }

        #region UseETopNetExceptionHandler, 例外處理器, 讓呼叫端能取得一致性的 json response
        /// <summary>
        /// 例外處理器, 讓呼叫端能取得一致性的 json response
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        /// <remark>Global Exception 的處理方式, 可參考: https://youtu.be/95EbHz3aKYA </remark>
        public static IApplicationBuilder UseProjectAExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(new ExceptionHandlerOptions
            {
                ExceptionHandler = (c) =>
                    {
                        var exception = c.Features.Get<IExceptionHandlerFeature>();

                        JsonResponse response = new JsonResponse();
                        // response.Status.Code = ApiStatusCode.ERR_EXCEPTION;
                        response.Status.Code = ApiStatusCode.ERR_EXCEPTION;
                        response.Status.Desc = exception.Error.Message;

                        byte[] content = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(response));
                        c.Response.StatusCode = (int)HttpStatusCode.OK;
                        c.Response.ContentType = "application/json";
                        c.Response.Body.WriteAsync(content);

                        return Task.CompletedTask;
                    }
            });

            return app;
        }
        #endregion

        #region UseSwagger, 使用 Swagger API 說明
        /// <summary>
        /// 使用 Swagger API 說明
        /// </summary>
        /// <param name="app"></param>
        /// <param name="url"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSwagger(this IApplicationBuilder app, string url, string name)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(url, name);
            });

            return app;
        }
        #endregion

        public static string AcceptLang(this HttpRequest httpRequest)
        {
            const string DEFAULT_LANG = "zh-TW";

            string? language = httpRequest.Headers["Accept-Language"].FirstOrDefault();
            if (string.IsNullOrEmpty(language))
                return DEFAULT_LANG;
            else
                return language;
        }

        #region 取得巢狀物件之實體ModelState名 （可能有bug在遇到陣列時）
        public static string NameOfFull<T>(this T obj, Expression<Func<T, object>> expression)
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
            {
                var unaryExpression = expression.Body as UnaryExpression;
                if (unaryExpression != null && unaryExpression.NodeType == ExpressionType.Convert)
                    memberExpression = unaryExpression.Operand as MemberExpression;
            }

            var result = memberExpression != null ? memberExpression.ToString() : "";
            result = result.Substring(result.IndexOf('.') + 1);

            return result;
        }

        public static string NameOfFull<T>(this T obj, string sourceFieldName, Expression<Func<T, object>> expression)
        {
            var result = obj.NameOfFull<T>(expression);
            result = string.IsNullOrEmpty(sourceFieldName) ? result : sourceFieldName + "." + result;
            return result;
        }
        #endregion
    }
}