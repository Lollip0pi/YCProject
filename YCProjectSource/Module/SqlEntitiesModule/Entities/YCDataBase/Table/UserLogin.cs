using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SqlEntitiesModule.Entities.YCDataBase.Table
{
    [Table("UserLogin")]
    public class UserLogin
    {
        /// <summary>
        /// 使用者Guid
        /// </summary>
        [Key]

        public Guid UserGuid { get; set; }

        /// <summary>
        /// 建立時間
        /// </summary>

        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 最後修改時間
        /// </summary>

        public DateTime? ModifyTime { get; set; }

        /// <summary>
        /// 是否登入 Y=是 N=否
        /// </summary>

        public string? isLogin { get; set; }

    }

}