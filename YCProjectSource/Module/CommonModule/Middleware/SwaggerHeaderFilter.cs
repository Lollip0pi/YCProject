using System;
using System.Collections.Generic;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Module.CommonModule.Middleware
{
    /// <summary>
    /// Swagger API Http Header 支援
    /// </summary>
    public class SwaggerHeaderFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<OpenApiParameter>();
            }
            IDictionary<string, OpenApiExample> clientIds = new Dictionary<string, OpenApiExample>();
            clientIds.Add("永慶 WEB", new OpenApiExample() { Value = new OpenApiString("YCWeb") });
            operation.Parameters.Add(
                new OpenApiParameter
                {
                    Name = "X-ClientID",
                    In = ParameterLocation.Header,
                    Required = true,
                    Schema = new OpenApiSchema { Type = "string" },
                    Examples = clientIds
                }
            );
            operation.Parameters.Add(
                new OpenApiParameter
                {
                    Name = "X-TXID",
                    In = ParameterLocation.Header,
                    Required = true,
                    Schema = new OpenApiSchema { Type = "string" },
                    Example = new OpenApiString("QS0001")
                }
            );
            IDictionary<string, OpenApiExample> langs = new Dictionary<string, OpenApiExample>();
            langs.Add("繁中", new OpenApiExample() { Value = new OpenApiString("zh-TW") });
            langs.Add("簡中", new OpenApiExample() { Value = new OpenApiString("zh-CN") });
            langs.Add("英文", new OpenApiExample() { Value = new OpenApiString("en-US") });
            operation.Parameters.Add(
                new OpenApiParameter
                {
                    Name = "Accept-Language",
                    In = ParameterLocation.Header,
                    Required = true,
                    Schema = new OpenApiSchema { Type = "string" },
                    Examples = langs
                }
            );
        }
    }
}