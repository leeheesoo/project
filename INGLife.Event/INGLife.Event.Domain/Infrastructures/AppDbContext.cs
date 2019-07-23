using INGLife.Event.Domain.Entities;
using INGLife.Event.Domain.Entities.KidsNote;
using INGLife.Event.Domain.Entities.MamboEvent;
using INGLife.Event.Domain.Entities.MarketingAgree;
using INGLife.Event.Domain.Entities.Rebranding;
using INGLife.Event.Domain.Entities.OverFortyFiveDb;
using INGLife.Event.Domain.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using INGLife.Event.Domain.Entities.FinancialConcertMarketingAgree;
using INGLife.Event.Domain.Entities.TumblerEntry;
using INGLife.Event.Domain.Entities.FinancialConsultantSharing;

namespace INGLife.Event.Domain.Infrastructures {    
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, long, AppUserLogin, AppUserRole, AppUserClaim> {
        public AppDbContext()
            : base("INGLife.Event") {

        }

        //제휴사
        public DbSet<Affiliates> Affiliates { get; set; }
        //이벤트관리
        public DbSet<EventManagement> EventManagement { get; set; }
        // 키즈노트 이벤트
        public DbSet<KidsNoteEntry> KidsNoteEntries { get; set; }
        // 닐리리맘보 이벤트
        public DbSet<NilririmanboEntry> NilririmanboEntries { get; set; }
        // 마케팅동의 캠페인
        public DbSet<MarketingAgreeEntry> MarketingAgreeEntries { get; set; }
        // 리브랜딩 캠페인
        public DbSet<RebrandingMarketingAgreeEntry> RebrandingMarketingAgreeEntries { get; set; }
        public DbSet<RebrandingConsultingAgreeEntry> RebrandingConsultingAgreeEntries { get; set; }

        // 4050DB확보 이벤트
        public DbSet<OverFortyFiveDbEntry> OverFortyFiveDbEntries { get; set; }

        //재무콘서트 마케팅동의
        public DbSet<FinancialConcertMarketingAgreeEntry> FinancialConcertMarketingAgreeEntries { get; set; }

        //텀블러
        public DbSet<TumblerEventEntry> TumblerEventEntry { get; set; }

        //재무상담사 텀블러
        public DbSet<FinancialConsultantEntry> FinancialConsultantEntry { get; set; }
        public DbSet<FinancialConsultantNewCustomerEntry> FinancialConsultantNewCustomerEntry { get; set; }
        public DbSet<FinancialConsultantOriginalCustomerEntry> FinancialConsultantOriginalCustomerEntry { get; set; }

        public static AppDbContext Create() {
            return new AppDbContext();
        }

        static AppDbContext() {
            Database.SetInitializer<AppDbContext>(null);
        }        

        public virtual void SetModified<TEntity>(TEntity entity) where TEntity : class {
            Entry<TEntity>(entity).State = EntityState.Modified;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>().ToTable("Users", "dbo");
            modelBuilder.Entity<AppUser>().Property(e => e.UserName).HasMaxLength(255).IsRequired();
            modelBuilder.Entity<AppRole>().ToTable("Roles", "dbo");
            modelBuilder.Entity<AppRole>().Property(e => e.Name).HasMaxLength(255).IsRequired();
            modelBuilder.Entity<AppUserRole>().ToTable("UserRoles", "dbo");
            modelBuilder.Entity<AppUserClaim>().ToTable("UserClaims", "dbo");
            modelBuilder.Entity<AppUserLogin>().ToTable("UserLogins", "dbo");
        }
    }
}