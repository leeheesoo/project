using KinderJoy.Domain.Identity;
using KinderJoy.Domain.Entities;
using KinderJoy.Domain.Entities.Word;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KinderJoy.Domain.Entities.Christmas2015;
using KinderJoy.Domain.Entities.BackToSchool2016;
using KinderJoy.Domain.Entities.ChildrensDay;
using KinderJoy.Domain.Entities.MainStream;
using KinderJoy.Domain.Entities.NinjaBarbie2016;
using KinderJoy.Domain.Entities.FindingDory2017;
using KinderJoy.Domain.Entities.MagicKinderAppLaunchingEvent;
using KinderJoy.Domain.Entities.MavelFrozen;
using MySql.Data.Entity;
using KinderJoy.Domain.Entities.KittyJusticeLeague;
using KinderJoy.Domain.Entities.Pororo2018;
using KinderJoy.Domain.Entities.DisneyStarWars2018;

namespace KinderJoy.Domain.Infrastructure
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class AppDbContext: IdentityDbContext<AppUser, AppRole, long, AppUserLogin, AppUserRole, AppUserClaim> {
        public virtual DbSet<PersonalInfos> PersonalInfos { get; set; }
        public virtual DbSet<WordEvent> WordEvents { get; set; }
        public virtual DbSet<WordEventShare> WordEventShares { get; set; }
        public virtual DbSet<WordWinner> WordWinners { get; set; }
        //2015년도 크리스마스이벤트
        public virtual DbSet<Christmas2015Win> Christmas2015Wins { get; set; }
        public virtual DbSet<Christmas2015WinPrizeSetting> Christmas2015WinPrizeSettings { get; set; }
        public virtual DbSet<Christmas2015MakeTree> Christmas2015MakeTrees { get; set; }
        public virtual DbSet<Christmas2015MakeTreeSNSShare> Christmas2015MakeTreeSNSShares { get; set; }
        // 2016년도 새학기이벤트
        public virtual DbSet<BackToSchool2016BingoQuiz> BackToSchool2016BingoQuiz { get; set; }
        public virtual DbSet<BackToSchool2016BingoQuizSns> BackToSchool2016BingoQuizSns { get; set; }
        // 어린이날 이벤트
        public virtual DbSet<ChildrensDayHiddenPicture> ChildrensDayHiddenPicture { get; set; }
        public virtual DbSet<ChildrensDayHiddenPictureSns> ChildrensDayHiddenPictureSns { get; set; }
        // 메인스트림 이벤트
        public virtual DbSet<MainStreamSurprise> MainStreamSurprise { get; set; }
        public virtual DbSet<MainStreamSurpriseSNS> MainStreamSurpriseSNS { get; set; }
        //2016닌자바비 이벤트
        public DbSet<NinjaBarbie2016Sharing> NinjaBarbie2016SnsSharing { get; set; }
        public DbSet<NinjaBarbie2016User> NinjaBarbie2016SnsSharingUser { get; set; }
        //2017 도리를찾아서 이벤트
        public virtual DbSet<FindingDory2017User> FindingDory2017Users { get; set; }
        public virtual DbSet<FindingDory2017SNS> FindingDory2017SNSs { get; set; }
        // 매직킨더앱런칭 이벤트
        public virtual DbSet<MagicKinderAppLaunching> MagicKinderLaunchings { get; set; }
        //2017 마블프로즌 이벤트
        public virtual DbSet<MavelFrozenUser> MavelFrozenUsers { get; set; }
        public virtual DbSet<MavelFrozenSNS> MavelFrozenSNSs { get; set; }
        //2017 키티&저스티스리그 이벤트
        public virtual DbSet<KittyJusticeLeagueInstantLottery> KittyJusticeLeagueInstantLotteies { get; set; }
        public virtual DbSet<KittyJusticeLeagueInstantLotteryPrizeSetting> KittyJusticeLeagueInstantLotteryPrizeSettings { get; set; }
        //2018 뽀로로 이벤트
        public virtual DbSet<Pororo2018InstantLottery> Pororo2018InstantLotteries { get; set; }
        public virtual DbSet<Pororo2018InstantLotteryPrizeSetting> Pororo2018InstantLotteryPrizeSettings { get; set; }
        //2018 디즈니&스타워즈 
        public virtual DbSet<DisneyStarWars2018InstantLottery> DisneyStarWars2018InstantLotteries { get; set; }
        public virtual DbSet<DisneyStarWars2018InstantLotteryPrizeSetting> DisneyStarWars2018InstantLotteryPrizeSettings { get; set; }

        public AppDbContext()
            : base("database.dsn"){
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
