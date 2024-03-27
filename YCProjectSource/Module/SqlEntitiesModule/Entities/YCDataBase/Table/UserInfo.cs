using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SqlEntitiesModule.Entities.YCDataBase.Table
{
    [Table("UserInfo")]
    public class UserInfo
    {
        /// <summary>
        /// 使用者Guid
        /// </summary>
        [Key]

        public Guid UserGuid { get; set; }

        /// <summary>
        /// 使用者戶名
        /// </summary>

        public string? UserName { get; set; }

        /// <summary>
        /// 使用者帳號
        /// </summary>

        public string? UserAcct { get; set; }

        /// <summary>
        /// 使用者密碼Hash
        /// </summary>

        public string? UserPass { get; set; }

        /// <summary>
        /// 狀態(1:有效、2:註銷)
        /// </summary>

        public string? Status { get; set; }

        /// <summary>
        /// 帳戶建立時間
        /// </summary>

        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 最後修改時間
        /// </summary>

        public DateTime? ModifyTime { get; set; }

    }

}