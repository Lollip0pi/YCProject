using System.Collections.Generic;

namespace DTOsModule.DTOs.Common
{
    public class BaseApiParam
    {
        /// <summary>
        /// 前端傳入之通道ID <br/>
        /// </summary>      
        public string? HeaderXClientID { get; set; }
        
        /// <summary>
        /// 前端傳入之執行序號 Guid（每次呼叫應為不同）
        /// </summary>      
        public string? HeaderXGUID { get; set; }

        /// <summary>
        /// 前端傳入之功能ID
        /// </summary>      
        public string? HeaderXTxID { get; set; }

        /// <summary>
        /// 前端傳入之語系代碼 zh-TW、zh-CN、en-US
        /// </summary>      
        public string? HeaderLanguage { get; set; }

        /// <summary>
        /// 登入票之帳戶身份唯一識別
        /// </summary>      
        public string? UserGuid { get; set; }

        /// <summary>
        /// 登入票之使用者姓名
        /// </summary>      
        public string? UserName { get; set; }

        /// <summary>
        /// 登入識別票有效與否
        /// </summary>      
        public bool IsAuthenticated { get; set; }

    }
}