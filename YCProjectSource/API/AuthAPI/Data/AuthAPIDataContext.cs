using Microsoft.EntityFrameworkCore;
using SqlEntitiesModule.Entities.YCDataBase.Table;

namespace AuthAPI.Data
{
    public class AuthAPIDataContext : DbContext
    {
        public AuthAPIDataContext(DbContextOptions<AuthAPIDataContext> options) : base(options)
        {

        }

        /// <summary>
        /// 用戶端DataContext
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 複合鍵要在此處做設定, 另需搭配 Model 使用 Key, Column annotation
            #region Table 資料
            #endregion Table 資料

        }
        // 使用者資訊
        public DbSet<UserInfo> UserInfos { get; set; }

        // 使用者登入
        public DbSet<UserLogin> UserLogins { get; set; }


    }
}