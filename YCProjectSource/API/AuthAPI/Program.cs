using AuthAPI.Extensions;
using CommonModule.Extensions;
using CommonModule.Middleware;
using CommonModule.Middlewares;
using CommonModule.Settings;
using DTOsModule.Extensions;
using Module.CommonModule.Common.Extensions;
using Microsoft.AspNetCore.Mvc;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory());
        string configPath = di.Parent.FullName;

        var Configbuilder = new ConfigurationBuilder()
                .SetBasePath(configPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables();

        var Configuration = Configbuilder.Build();

        // swagger
        builder.Services.AddSwaggerService("AuthAPI 永慶", "Ver 001");
        
        // 停用 Client Error ( http status = 400 ), 改在 controller 檢查 ModelState
        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        // Cors
        builder.Services.AddCors();
        builder.Services.AddCorsPolicy(Configuration);

        // add options
        builder.Services.Configure<AuthModuleSetting>(Configuration.GetSection("AuthModule"));
        

        // add service
        const string YCDataBaseDB = "YCDataBaseDB";
        builder.Services.AddCommonModuleServices(Configuration, YCDataBaseDB);//注冊共用服務
        builder.Services.AddDTOsModuleServices(Configuration);//註冊 DTOs  服務
        builder.Services.AddModuleServices_AuthAPI(Configuration, YCDataBaseDB);//注冊使用者登入服務

        // add controller
        builder.Services.AddControllers()
            .AddJsonOptions(Options =>
            {
                Options.JsonSerializerOptions.PropertyNamingPolicy = null;
                Options.JsonSerializerOptions.IgnoreNullValues = true;
            });


        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddHttpClient("HttpsConnect", c =>
            {
                c.BaseAddress = new Uri("https://localhost/");
            }).ConfigurePrimaryHttpMessageHandler(h =>
            {
                var handler = new HttpClientHandler();

                handler.ServerCertificateCustomValidationCallback = delegate { return true; };
                return handler;
            });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "AuthAPI 永慶");
            });
        }

        // 新增可以取得 XForwardedFor Remote Ip Address
        app.UseCommonApplicationBuilder();
        // 2021/10/17, modify by chien, Missing HSTS Header
        app.UseHsts();
        // 讓 webview & 可以跨網站存取
        app.UseCorsPolicy();

        app.UseRouting();

        app.UseMiddleware<JwtMiddleware>();
        app.UseMiddleware<CommHeaderMiddleware>();

        app.UseProjectAExceptionHandler();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.Run();
    }
}