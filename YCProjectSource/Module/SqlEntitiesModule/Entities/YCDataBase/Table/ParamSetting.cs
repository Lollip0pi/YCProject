using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SqlEntitiesModule.Entities.YCDataBase.Table
{
    [Table("ParamSetting")]
    public class ParamSetting
    {
        /// <summary>
        /// 功能類別
        /// </summary>
        [Key]
        public string? FuncType { get; set; }
        /// <summary>
        /// 參數類別
        /// </summary>
        [Key] 
        public string? Param { get; set; }
        /// <summary>
        /// 參數類別敘述
        /// </summary>
        public string? ParamDesc { get; set; }
        /// <summary>
        /// 代碼
        /// </summary>
        [Key]
        public string? Code { get; set; }
        /// <summary>
        /// 代碼敘述
        /// </summary>
        public string? CodeDesc { get; set; }

    }
}