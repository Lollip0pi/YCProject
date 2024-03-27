using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DTOsModule.Extensions
{
    public static class DTOsModuleExtension
    {
        #region AddDTOsModuleServices, 註冊共用服務
        /// <summary>
        /// 註冊共用服務
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <param name="connectionStr"></param>
        /// <returns></returns>
        public static IServiceCollection AddDTOsModuleServices(this IServiceCollection services, IConfiguration config)
        {
            return services;
        }
        #endregion
        
    }
}