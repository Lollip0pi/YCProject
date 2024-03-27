using System.Data;
using Module.CommonModule.Interfaces;
using CommonModule.Data;
using Microsoft.EntityFrameworkCore;
using SqlEntitiesModule.Entities.YCDataBase.Table;

namespace Module.CommonModule.Services
{
    public class CommonService : ICommonService
    {
        private readonly ILogger<CommonService> _logger;
        private readonly IConfiguration _config;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly CommDataContext _CommDataContext;

        public CommonService(ILogger<CommonService> logger
            , IConfiguration config
            , IHttpClientFactory httpClientFactory
            , CommDataContext CommDataContext
         )
        {
            _logger = logger;
            _config = config;
            _httpClientFactory = httpClientFactory;
            _CommDataContext = CommDataContext;
        }

        #region GetStatusCodeMapping
        /// <summary>
        /// 取得狀態碼的多語系對應
        /// </summary>
        /// <param name="source">來源</param>
        /// <param name="code">代碼</param>
        /// <param name="localeId">語系</param>
        /// <returns>多語系描述</returns>
        public string GetStatusCodeMapping(string source, string code, string localeId)
        {
            // 1. 如表中無此代碼描述, 則返回代碼
            // 2. 預設語系的描述不會出現在語系描述中, 會重複出現在 Desc 欄位
            // 3. 語系的描述要讀 LocaleDesc 欄位
            string codeDesc = MappingResource("WEBAPI", source, code, localeId, code);
            return codeDesc;
        }
        #endregion
        #region Mapping Resource 查詢參數敘述
        /// <summary>
        /// Mapping Resource 查詢參數敘述
        /// </summary>
        public string MappingResource(string FuncType, string Param, string Code, string LocaleId, string DefaultDesc)
        {
            try
            {
                string result = _CommDataContext.ParamSettings.Where(m => m.FuncType == FuncType
                    && m.Param == Param && m.Code == Code).AsNoTracking().FirstOrDefault().CodeDesc;

                if(string.IsNullOrEmpty(result))
                {
                    result = "";
                }

                return result;
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        #endregion

    }
}