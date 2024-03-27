using System.Reflection;
using AAAPI.Services;
using AuthAPI.Data;
using AuthAPI.Interfaces;
using AuthAPI.Profiles;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Extensions
{
    public static class AuthAPIExtension
    {
        #region AddModuleServices_AuthAPI, 註冊共用服務
        /// <summary>
        /// 註冊共用服務
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <param name="YCDataBaseDB"></param>
        /// <returns></returns>
        public static IServiceCollection AddModuleServices_AuthAPI(this IServiceCollection services, IConfiguration config
            , string YCDataBaseDB)
        {
            #region 註冊資料庫
            string YCDataBase = config.GetConnectionString(YCDataBaseDB);
            services.AddDbContext<AuthAPIDataContext>(p => p.UseSqlServer(YCDataBase));
            #endregion 註冊資料庫

            #region 註冊服務
            services.AddScoped<IAuthService, AuthService>();//使用者登入/註冊
            #endregion 註冊服務

            #region 註冊Profile
            List<Assembly> ass = new List<Assembly>();
            ass.Add(Assembly.GetAssembly(typeof(AuthProfile)));//使用者登入/註冊
            services.AddAutoMapper(ass);
            #endregion 註冊Profile

            return services;
        }
        #endregion


    }
}