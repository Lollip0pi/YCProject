using System.Security.Cryptography;
using System.Text;

namespace CommonModule.Utilities
{
    /// <summary>
    /// 加密解密
    /// </summary>
    public class CryptyUtilities
    {
        #region Encrypt 實作加密
        public static string Encrypt(string encrypt_Str)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(encrypt_Str));

            var sb = new StringBuilder();
            for(int i =0; i<bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("x2"));
            }
            return sb.ToString();

        }
        #endregion
    }
}