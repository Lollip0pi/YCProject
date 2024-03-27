using Microsoft.EntityFrameworkCore;
using SqlEntitiesModule.Entities.YCDataBase.Table;

namespace CommonModule.Data
{
    public class CommDataContext : DbContext
    {
        public CommDataContext(DbContextOptions<CommDataContext> options) : base(options)
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
            modelBuilder.Entity<ParamSetting>()
                    .HasKey(e => new { e.FuncType, e.Param, e.Code });
            #endregion Table 資料

        }
        // 使用者資訊
        public DbSet<ParamSetting> ParamSettings { get; set; }


    }
}