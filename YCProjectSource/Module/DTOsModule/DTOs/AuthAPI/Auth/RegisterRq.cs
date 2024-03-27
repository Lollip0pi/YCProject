using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DTOsModule.DTOs.AuthAPI.Auth
{
    /// <summary>
    /// Register Response DTO
    /// </summary>
    public class RegisterRq
    {
        /// <summary>
        /// 使用者名字
        /// </summary>
        /// <value></value>
        [Required]
        [MinLength(1)]
        [MaxLength(20)]
        public string? UserName { get; set; }

        /// <summary>
        /// 使用者帳號
        /// </summary>
        /// <value></value>
        [Required]
        [MinLength(8)]
        [MaxLength(20)]
        public string? UserAcct { get; set; }
        
        /// <summary>
        /// 使用者密碼
        /// </summary>
        /// <value></value>
        [Required]
        [MinLength(8)]
        [MaxLength(20)]
        public string? UserPass { get; set; }

    }
}