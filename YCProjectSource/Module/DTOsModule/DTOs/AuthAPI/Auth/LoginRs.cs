using System.Collections.Generic;

namespace DTOsModule.DTOs.AuthAPI.Auth
{
    /// <summary>
    /// Login Response DTO
    /// </summary>
    public class LoginRs
    {
        public string StatusCode { get; set; }
        
        /// <summary>
        /// 存取令牌
        /// </summary>
        /// <value></value>
        public string? AccessToken { get; set; }
        /// <summary>
        /// 使用者姓名
        /// </summary>
        /// <value></value>
        public string? UserName { get; set; }
        /// <summary>
        /// 使用者Id
        /// </summary>
        /// <value></value>
        public string? UserId { get; set; }
    }
}