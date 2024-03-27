using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.RegularExpressions;
using System.Reflection;
using Module.CommonModule.Interfaces;
using Module.CommonModule.Services;
using Module.CommonModule.Middleware;
using CommonModule.Data;

namespace Module.CommonModule.Common.Extensions
{
    public static class InitServiceExtension
    {
        #region AddCommonModuleServices, 註冊共用服務
        /// <summary>
        /// 註冊共用服務
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <param name="YCDataBaseDB"></param>
        /// <returns></returns>
        public static IServiceCollection AddCommonModuleServices(this IServiceCollection services, IConfiguration config, string YCDataBaseDB)
        {
            string YCDataBase = config.GetConnectionString(YCDataBaseDB);
            services.AddDbContext<CommDataContext>(p => p.UseSqlServer(YCDataBase));
            services.AddScoped<DbContext, CommDataContext>();

            services.AddScoped<ICommonService, CommonService>();

            List<Assembly> ass = new List<Assembly>();
            services.AddAutoMapper(ass);
            return services;
        }
        #endregion

        public static IApplicationBuilder UseCommonApplicationBuilder(this IApplicationBuilder app)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            return app;
        }

        #region AddSwaggerService, 新增 Swagger API 支援
        /// <summary>
        /// 新增 Swagger API 支援
        /// </summary>
        /// <param name="services"></param>
        /// <param name="title"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerService(this IServiceCollection services, string title = "AAAPI", string version = "v1")
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = title, Version = version, });
                c.OperationFilter<SwaggerHeaderFilter>();
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. <br/> 
                                    Enter 'Bearer' [space] and then your token in the text input below.<br/>",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });

                // 以目前執行的目錄, 載入所有已 compiler 出的程式碼註解
                string path = AppContext.BaseDirectory;
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                FileInfo[] files = directoryInfo.GetFiles();
                foreach (FileInfo fileInfo in files)
                {
                    if (string.Compare(fileInfo.Extension, ".xml", true) == 0)
                    {
                        c.IncludeXmlComments(fileInfo.FullName);
                    }
                }
            });
            return services;
        }
        #endregion


        private const string CORS_POLICY = "CorsPolicy";

        #region UseCorsPolicy, APP 使用 CORS Policy
        /// <summary>
        /// APP 使用 CORS Policy
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCorsPolicy(this IApplicationBuilder app)
        {
            app.UseCors(CORS_POLICY);
            return app;
        }
        #endregion 

        private static string[] _corsList;

        #region AddCorsPolicy, 新增 CORS Policy Service
        /// <summary>
        /// 新增 CORS Policy Service
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration config)
        {
            // 稍後寫入Log
            // var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            _corsList = config.GetSection("CommonModule:CORSList").Get<string[]>();
            // logger.Info("corsList=" + String.Join(",", _corsList));

            services.AddCors(options =>
            {
                options.AddPolicy(CORS_POLICY,
                    builder => builder.SetIsOriginAllowed(IsOriginAllowed).AllowAnyMethod().AllowAnyHeader());
            });

            return services;
        }

        /// <summary>
        /// 自定義 CORS IP or Domain name Check
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        private static bool IsOriginAllowed(string host)
        {
            bool isAllow = _corsList.Any(origin =>
                Regex.IsMatch(host, $@"^http(s)?://.*{origin}(:[0-9]+)?$", RegexOptions.IgnoreCase));

            if (!isAllow)
            {
                var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
                logger.Warn($"CORS Check, {host} is not allowed!!");
            }

            return isAllow;
        }

        #endregion

    }
}