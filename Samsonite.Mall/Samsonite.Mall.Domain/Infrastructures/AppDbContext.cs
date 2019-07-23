using Samsonite.Mall.Domain.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using MySql.Data.Entity;
using Samsonite.Mall.Domain.Entities.OneYearAnniversary;
using Samsonite.Mall.Domain.Entities.BagtotheFuture;
using Samsonite.Mall.Domain.Entities.Christmas2017;
using Samsonite.Mall.Domain.Entities.TwoYearAnniversary;
using Samsonite.Mall.Domain.Entities.Abstract;

namespace Samsonite.Mall.Domain.Infrastructures {
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class AppDbContext: IdentityDbContext<AppUser, AppRole, long, AppUserLogin, AppUserRole, AppUserClaim> {        
        //오행시짓기 댓글이벤트
        public DbSet<OneYearAnniversaryEntry> OneYearAnniversaryEntry { get; set; }
        //백투더퓨처 아이디어 공모전 이벤트 - 공모전 참여지정보
        public DbSet<BagtotheFutureEntry> BagtotheFutureEntry { get; set; }
        //백투더퓨처 아이디어 공모전 이벤트 - sns 참여자정보
        public DbSet<BagtotheFutureSnsUser> BagtotheFutureSnsUser { get; set; }
        //백투더퓨처 아이디어 공모전 이벤트 - sns 공유정보
        public DbSet<BagtotheFutureSns> BagtotheFutureSns { get; set; }
        //2017크리스마스 이벤트
        public DbSet<Christmas2017Entry> Christmas2017Entries { get; set; }
        //샘소몰 2주년 이벤트
        public DbSet<TwoYearAnniversaryEntry> TwoYearAnniversaryEntries { get; set; }
        public DbSet<TwoYearAnniversaryInstantPrizeSetting> TwoYearAnniversaryInstantPrizeSettings { get; set; }
        public DbSet<TwoYearAnniversaryWinCoupon> TwoYearAnniversaryWinCoupons { get; set; }

        public AppDbContext()
                : base("Samsonite.Mall") {
        }
        static AppDbContext() {
            Database.SetInitializer<AppDbContext>(null);
        }
        public static AppDbContext Create() {
            return new AppDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            //Identity DB create
            modelBuilder.Entity<AppUser>().Property(e => e.UserName).HasMaxLength(255).IsRequired();
            modelBuilder.Entity<AppUser>().ToTable("Users", "dbo");
            modelBuilder.Entity<AppRole>().Property(e => e.Name).HasMaxLength(255).IsRequired();
            modelBuilder.Entity<AppRole>().ToTable("Roles", "dbo");
            modelBuilder.Entity<AppUserRole>().ToTable("UserRoles", "dbo");
            modelBuilder.Entity<AppUserClaim>().ToTable("UserClaims", "dbo");
            modelBuilder.Entity<AppUserLogin>().ToTable("UserLogins", "dbo");
        }
    }
}
