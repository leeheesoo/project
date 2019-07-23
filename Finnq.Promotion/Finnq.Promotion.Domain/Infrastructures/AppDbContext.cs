using Finnq.Promotion.Domain.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using MySql.Data.Entity;
using Finnq.Promotion.Domain.Entities.TmapEvent;
using Finnq.Promotion.Domain.Entities.RouletteEvent;
using Finnq.Promotion.Domain.Entities.TWorldRouletteEvent;

namespace Finnq.Promotion.Domain.Infrastructures {
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, long, AppUserLogin, AppUserRole, AppUserClaim> {
        public AppDbContext()
                : base("Finnq.Promotion") {
        }
        static AppDbContext() {
            Database.SetInitializer<AppDbContext>(null);
        }
        public static AppDbContext Create() {
            return new AppDbContext();
        }

        public DbSet<TmapEventEntry> TmapEventEntry { get; set; }


        // 룰렛이벤트
        public DbSet<RouletteEventEntry> RouletteEventEntries { get; set; }
        public DbSet<RouletteEventInstantLotteryPrizeSetting> RouletteEventInstantLotteryPrizeSettings { get; set; }


        // T-룰렛이벤트
        public DbSet<TRouletteEventEntry> TRouletteEventEntries { get; set; }
        public DbSet<TRouletteEventInstantLotteryPrizeSetting> TRouletteEventInstantLotteryPrizeSettings { get; set; }


        // TWorld-룰렛이벤트
        public DbSet<TWorldRouletteEventEntry> TWorldRouletteEventEntries { get; set; }
        public DbSet<TWorldRouletteEventInstantLotteryPrizeSetting> TWorldRouletteEventInstantLotteryPrizeSettings { get; set; }

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
