namespace CommonModule.Settings
{
    /// <summary>
    /// AuthModule 設定, 會讀 appsettings.json 的 AuthModule 區段
    /// </summary>
    public class AuthModuleSetting
    {
        /// <summary>
        /// Bearer Token Timeout (秒), 用 token 可以存取限制資源, 所以要設定短一點
        /// </summary>
        /// <value></value>
        public int TokenTimeout { get; set; }

        /// <summary>
        /// 簽 token 所使用的 KK
        /// </summary>
        /// <value></value>
        public string TokenKK { get; set; }

        /// <summary>
        /// Bearer Token 發行者
        /// </summary>
        /// <value></value>
        public string TokenIssuer { get; set; }
        public string TokenAudience { get; set; }
    }
}