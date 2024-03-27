using System.ComponentModel.DataAnnotations;

namespace DTOsModule.DTOs.AuthAPI.Auth
{
    /// <summary>
    /// Login Request DTO
    /// </summary>
    public class LoginRq
    {
        /// <summary>
        /// 使用者帳號
        /// </summary>
        /// <value></value>
        [Required]
        [MinLength(8)]
        [MaxLength(16)]
        public string? UserAcct { get; set; }
        /// <summary>
        /// 使用者密碼
        /// </summary>
        /// <value></value>
        [Required]
        [MinLength(8)]
        [MaxLength(16)]
        public string? UserPass { get; set; }

    }
}