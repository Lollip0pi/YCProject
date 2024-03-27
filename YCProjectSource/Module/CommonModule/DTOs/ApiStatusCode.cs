namespace CommonModule.DTOs
{
    public class ApiStatusCode
    {
        #region 平台共用類錯誤代碼

        #region 系統類
        /// <summary>
        /// 成功
        /// </summary>
        public const string SUCCESS = "0";                              // 成功 

        /// <summary>
        /// 資料庫存取錯誤
        /// </summary>
        public const string ERR_DB_ACCESS_ERROR = "CM001";               // 資料庫存取錯誤

        /// <summary>
        /// 資料驗證錯誤
        /// </summary>
        public const string ERR_DATA_VALIDATE = "CM002";                 // 資料驗證錯誤

        /// <summary>
        /// Http Header 共用區必要欄位不齊全
        /// </summary>
        public const string ERR_HEADER_VALIDATE = "CM003";              // Http Header 共用區必要欄位不齊全

        /// <summary>
        /// Http Header 共用區值不合規定
        /// </summary>
        public const string ERR_HEADER_INVALID_VALUE = "CM004";         // Http Header 共用區值不合規定

        /// <summary>
        /// 系統異常
        /// </summary>
        public const string ERR_EXCEPTION = "CM9999";                   // 系統異常
        #endregion 系統類
        
        #region 使用者登入類
        /// <summary>
        /// 使用者密碼輸入錯誤
        /// </summary>
        public const string ERR_AUTH_USERPASS_ERROR = "AU001";                // 使用者密碼輸入錯誤

        /// <summary>
        /// 使用者電子郵件已註冊
        /// </summary>
        public const string ERR_EMAIL_EXISTED = "AU002";              // 使用者電子郵件已註冊

        /// <summary>
        /// 使用者暱稱已存在
        /// </summary>
        public const string ERR_USERID_EXISTED = "AU003";              // 使用者暱稱已存在

        /// <summary>
        /// 使用者電子郵件不存在
        /// </summary>
        public const string ERR_EMAIL_NOT_EXIST = "AU004";              // 使用者電子郵件不存在

        /// <summary>
        /// 重設密碼Token失效
        /// </summary>
        public const string ERR_RESETTOKEN_DISMISS = "AU005";              // 重設密碼Token失效
        #endregion 使用者登入類
        
        #endregion 平台共用類錯誤代碼
    }
}