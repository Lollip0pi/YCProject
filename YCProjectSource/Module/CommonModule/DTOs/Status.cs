namespace Module.CommonModule.DTOs
{
    /// <summary>
    /// API 結果 DTO
    /// </summary>
    public class Status
    {
        /// <summary>
        /// 代碼
        /// </summary>
        /// <value></value>
        public string? Code { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        /// <value></value>
        public string? Desc { get; set; }
        public Dictionary<string, List<string>>? Errors { get; set; }

        /// <summary>
        /// 時間
        /// </summary>
        /// <value></value>
        public string? TrxTime { get; set; }
    }
}