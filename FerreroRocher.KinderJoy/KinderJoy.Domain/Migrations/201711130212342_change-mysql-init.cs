namespace KinderJoy.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changemysqlinit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BackToSchool2016BingoQuiz",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Age = c.Int(nullable: false),
                        Mobile = c.String(nullable: false, maxLength: 11, storeType: "nvarchar"),
                        ZipCode = c.String(nullable: false, maxLength: 5, storeType: "nvarchar"),
                        Address1 = c.String(nullable: false, maxLength: 300, storeType: "nvarchar"),
                        Address2 = c.String(nullable: false, maxLength: 300, storeType: "nvarchar"),
                        Channel = c.String(nullable: false, maxLength: 10, storeType: "nvarchar"),
                        IpAddress = c.String(nullable: false, maxLength: 15, storeType: "nvarchar"),
                        CreateDate = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BackToSchool2016BingoQuizSns",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        BackToSchool2016BingoQuizId = c.Long(nullable: false),
                        SnsType = c.String(maxLength: 20, storeType: "nvarchar"),
                        SnsId = c.String(maxLength: 100, storeType: "nvarchar"),
                        SnsName = c.String(maxLength: 50, storeType: "nvarchar"),
                        SnsNickName = c.String(maxLength: 50, storeType: "nvarchar"),
                        ScrapUrl = c.String(maxLength: 200, storeType: "nvarchar"),
                        RegisterDate = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BackToSchool2016BingoQuiz", t => t.BackToSchool2016BingoQuizId, cascadeDelete: true)
                .Index(t => t.BackToSchool2016BingoQuizId);
            
            CreateTable(
                "dbo.ChildrensDayHiddenPictures",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Age = c.Int(nullable: false),
                        Mobile = c.String(nullable: false, maxLength: 11, storeType: "nvarchar"),
                        Gender = c.String(nullable: false, maxLength: 3, storeType: "nvarchar"),
                        Channel = c.String(nullable: false, maxLength: 10, storeType: "nvarchar"),
                        IpAddress = c.String(nullable: false, maxLength: 15, storeType: "nvarchar"),
                        CreateDate = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ChildrensDayHiddenPictureSns",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ChildrensDayHiddenPictureId = c.Long(nullable: false),
                        SnsType = c.String(maxLength: 20, storeType: "nvarchar"),
                        SnsId = c.String(maxLength: 100, storeType: "nvarchar"),
                        SnsName = c.String(maxLength: 50, storeType: "nvarchar"),
                        SnsNickName = c.String(maxLength: 50, storeType: "nvarchar"),
                        ScrapUrl = c.String(maxLength: 200, storeType: "nvarchar"),
                        RegisterDate = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChildrensDayHiddenPictures", t => t.ChildrensDayHiddenPictureId, cascadeDelete: true)
                .Index(t => t.ChildrensDayHiddenPictureId);
            
            CreateTable(
                "dbo.Christmas2015MakeTree",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Channel = c.String(nullable: false, unicode: false),
                        IpAddress = c.String(nullable: false, unicode: false),
                        RegisterDate = c.DateTime(nullable: false, precision: 0),
                        Name = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Mobile = c.String(nullable: false, maxLength: 25, storeType: "nvarchar"),
                        Zipcode = c.String(nullable: false, maxLength: 7, storeType: "nvarchar"),
                        Address1 = c.String(nullable: false, maxLength: 250, storeType: "nvarchar"),
                        Address2 = c.String(nullable: false, maxLength: 250, storeType: "nvarchar"),
                        Age = c.Int(nullable: false),
                        Toy1 = c.Int(),
                        Toy2 = c.Int(),
                        Toy3 = c.Int(),
                        Toy4 = c.Int(),
                        Toy5 = c.Int(),
                        Toy6 = c.Int(),
                        Toy7 = c.Int(),
                        SynthesisImage = c.String(unicode: false),
                        Content = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Christmas2015MakeTreeSNSShare",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Christmas2015MakeTreeId = c.Long(nullable: false),
                        SnsType = c.String(maxLength: 20, storeType: "nvarchar"),
                        SnsId = c.String(maxLength: 100, storeType: "nvarchar"),
                        SnsName = c.String(maxLength: 50, storeType: "nvarchar"),
                        SnsNickName = c.String(maxLength: 50, storeType: "nvarchar"),
                        ScrapUrl = c.String(maxLength: 200, storeType: "nvarchar"),
                        RegisterDate = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Christmas2015MakeTree", t => t.Christmas2015MakeTreeId, cascadeDelete: true)
                .Index(t => t.Christmas2015MakeTreeId);
            
            CreateTable(
                "dbo.Christmas2015WinPrizeSetting",
                c => new
                    {
                        Date = c.DateTime(nullable: false, precision: 0),
                        PrizeType = c.Int(nullable: false),
                        Rate = c.Single(nullable: false),
                        TotalCount = c.Int(nullable: false),
                        WinnerCount = c.Int(nullable: false),
                        StartTime = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Date, t.PrizeType });
            
            CreateTable(
                "dbo.Christmas2015Win",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        PrizeType = c.Int(nullable: false),
                        Channel = c.String(nullable: false, unicode: false),
                        IpAddress = c.String(nullable: false, unicode: false),
                        RegisterDate = c.DateTime(nullable: false, precision: 0),
                        Name = c.String(maxLength: 128, storeType: "nvarchar"),
                        Mobile = c.String(maxLength: 25, storeType: "nvarchar"),
                        Zipcode = c.String(maxLength: 7, storeType: "nvarchar"),
                        Address1 = c.String(maxLength: 250, storeType: "nvarchar"),
                        Address2 = c.String(maxLength: 250, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FindingDory2017SNS",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        SnsType = c.Int(nullable: false),
                        SnsId = c.String(maxLength: 100, storeType: "nvarchar"),
                        SnsName = c.String(maxLength: 100, storeType: "nvarchar"),
                        Post = c.String(maxLength: 300, storeType: "nvarchar"),
                        CreateDate = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FindingDory2017User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.FindingDory2017User",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Age = c.Int(nullable: false),
                        ZipCode = c.String(nullable: false, maxLength: 5, storeType: "nvarchar"),
                        Address = c.String(nullable: false, maxLength: 300, storeType: "nvarchar"),
                        AddressDetail = c.String(nullable: false, maxLength: 300, storeType: "nvarchar"),
                        Channel = c.String(nullable: false, maxLength: 10, storeType: "nvarchar"),
                        IpAddress = c.String(nullable: false, unicode: false),
                        Name = c.String(nullable: false, maxLength: 10, storeType: "nvarchar"),
                        Mobile = c.String(nullable: false, maxLength: 13, storeType: "nvarchar"),
                        CreateDate = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MagicKinderAppLaunchings",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ScreenShot = c.String(nullable: false, unicode: false),
                        ScreenShotType = c.Int(nullable: false),
                        Comment = c.String(nullable: false, unicode: false),
                        Age = c.Int(nullable: false),
                        ZipCode = c.String(nullable: false, maxLength: 5, storeType: "nvarchar"),
                        Address = c.String(nullable: false, maxLength: 300, storeType: "nvarchar"),
                        AddressDetail = c.String(nullable: false, maxLength: 300, storeType: "nvarchar"),
                        Channel = c.String(nullable: false, maxLength: 10, storeType: "nvarchar"),
                        IpAddress = c.String(nullable: false, unicode: false),
                        IsShow = c.Boolean(nullable: false),
                        Name = c.String(nullable: false, maxLength: 10, storeType: "nvarchar"),
                        Mobile = c.String(nullable: false, maxLength: 13, storeType: "nvarchar"),
                        CreateDate = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MainStreamSurprises",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Channel = c.String(nullable: false, maxLength: 10, storeType: "nvarchar"),
                        Name = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Age = c.Int(nullable: false),
                        Mobile = c.String(nullable: false, maxLength: 11, storeType: "nvarchar"),
                        Gender = c.String(nullable: false, maxLength: 3, storeType: "nvarchar"),
                        Quiz = c.Int(nullable: false),
                        IpAddress = c.String(nullable: false, maxLength: 15, storeType: "nvarchar"),
                        CreateDate = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MainStreamSurpriseSNS",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        MainStreamSurpriseId = c.Long(nullable: false),
                        SnsType = c.String(maxLength: 20, storeType: "nvarchar"),
                        SnsId = c.String(maxLength: 100, storeType: "nvarchar"),
                        SnsName = c.String(maxLength: 50, storeType: "nvarchar"),
                        SnsNickName = c.String(maxLength: 50, storeType: "nvarchar"),
                        ScrapUrl = c.String(maxLength: 200, storeType: "nvarchar"),
                        RegisterDate = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MainStreamSurprises", t => t.MainStreamSurpriseId, cascadeDelete: true)
                .Index(t => t.MainStreamSurpriseId);
            
            CreateTable(
                "dbo.MavelFrozenSNS",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        SnsType = c.Int(nullable: false),
                        SnsId = c.String(maxLength: 100, storeType: "nvarchar"),
                        SnsName = c.String(maxLength: 100, storeType: "nvarchar"),
                        Post = c.String(maxLength: 300, storeType: "nvarchar"),
                        CreateDate = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MavelFrozenUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.MavelFrozenUsers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Age = c.Int(nullable: false),
                        ZipCode = c.String(nullable: false, maxLength: 5, storeType: "nvarchar"),
                        Address = c.String(nullable: false, maxLength: 300, storeType: "nvarchar"),
                        AddressDetail = c.String(nullable: false, maxLength: 300, storeType: "nvarchar"),
                        ChildGender = c.Int(nullable: false),
                        Channel = c.String(nullable: false, maxLength: 10, storeType: "nvarchar"),
                        IpAddress = c.String(nullable: false, unicode: false),
                        Name = c.String(nullable: false, maxLength: 10, storeType: "nvarchar"),
                        Mobile = c.String(nullable: false, maxLength: 13, storeType: "nvarchar"),
                        CreateDate = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NinjaBarbie2016Sharing",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        SnsType = c.Int(nullable: false),
                        SnsId = c.String(maxLength: 100, storeType: "nvarchar"),
                        SnsName = c.String(maxLength: 100, storeType: "nvarchar"),
                        Post = c.String(maxLength: 300, storeType: "nvarchar"),
                        CreateDate = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NinjaBarbie2016User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.NinjaBarbie2016User",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Channel = c.String(nullable: false, maxLength: 10, storeType: "nvarchar"),
                        ZipCode = c.String(nullable: false, maxLength: 5, storeType: "nvarchar"),
                        Address = c.String(nullable: false, maxLength: 300, storeType: "nvarchar"),
                        AddressDetail = c.String(nullable: false, maxLength: 300, storeType: "nvarchar"),
                        Age = c.Int(nullable: false),
                        SurprizeType = c.Int(nullable: false),
                        IpAddress = c.String(nullable: false, unicode: false),
                        Top = c.Int(),
                        Bottom = c.Int(),
                        Accessory = c.Int(),
                        FacebookImage = c.String(unicode: false),
                        KakaotalkImage = c.String(unicode: false),
                        KakaostoryImage = c.String(unicode: false),
                        Name = c.String(nullable: false, maxLength: 10, storeType: "nvarchar"),
                        Mobile = c.String(nullable: false, maxLength: 13, storeType: "nvarchar"),
                        CreateDate = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PersonalInfos",
                c => new
                    {
                        PersonalInfoId = c.Int(nullable: false, identity: true),
                        EventId = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Name = c.String(maxLength: 128, storeType: "nvarchar"),
                        Mobile = c.String(maxLength: 25, storeType: "nvarchar"),
                        Zipcode = c.String(maxLength: 10, storeType: "nvarchar"),
                        Address1 = c.String(maxLength: 250, storeType: "nvarchar"),
                        Address2 = c.String(maxLength: 250, storeType: "nvarchar"),
                        Age = c.Int(nullable: false),
                        Gender = c.String(maxLength: 1, storeType: "nvarchar"),
                        Email = c.String(maxLength: 250, storeType: "nvarchar"),
                        IpAddress = c.String(maxLength: 20, storeType: "nvarchar"),
                        RegisterDate = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.PersonalInfoId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.Long(nullable: false),
                        RoleId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Email = c.String(maxLength: 256, storeType: "nvarchar"),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(unicode: false),
                        SecurityStamp = c.String(unicode: false),
                        PhoneNumber = c.String(unicode: false),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(precision: 0),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        ClaimType = c.String(unicode: false),
                        ClaimValue = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ProviderKey = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        UserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.WordEvents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PersonalInfoId = c.Int(nullable: false),
                        GiftType = c.String(nullable: false, maxLength: 1, storeType: "nvarchar"),
                        Ip = c.String(maxLength: 16, storeType: "nvarchar"),
                        Channel = c.String(maxLength: 7, storeType: "nvarchar"),
                        St = c.Int(nullable: false),
                        RegisteredAt = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PersonalInfos", t => t.PersonalInfoId, cascadeDelete: true)
                .Index(t => t.PersonalInfoId);
            
            CreateTable(
                "dbo.WordEventShares",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WordEventId = c.Int(nullable: false),
                        SnsType = c.String(maxLength: 20, storeType: "nvarchar"),
                        SnsId = c.String(maxLength: 100, storeType: "nvarchar"),
                        PostId = c.String(maxLength: 200, storeType: "nvarchar"),
                        Ip = c.String(maxLength: 16, storeType: "nvarchar"),
                        Channel = c.String(maxLength: 7, storeType: "nvarchar"),
                        RegisteredAt = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WordEvents", t => t.WordEventId, cascadeDelete: true)
                .Index(t => t.WordEventId);
            
            CreateTable(
                "dbo.WordWinners",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        St = c.Int(nullable: false),
                        GiftType = c.String(nullable: false, maxLength: 1, storeType: "nvarchar"),
                        Name = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        Mobile = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WordEventShares", "WordEventId", "dbo.WordEvents");
            DropForeignKey("dbo.WordEvents", "PersonalInfoId", "dbo.PersonalInfos");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.NinjaBarbie2016Sharing", "UserId", "dbo.NinjaBarbie2016User");
            DropForeignKey("dbo.MavelFrozenSNS", "UserId", "dbo.MavelFrozenUsers");
            DropForeignKey("dbo.MainStreamSurpriseSNS", "MainStreamSurpriseId", "dbo.MainStreamSurprises");
            DropForeignKey("dbo.FindingDory2017SNS", "UserId", "dbo.FindingDory2017User");
            DropForeignKey("dbo.Christmas2015MakeTreeSNSShare", "Christmas2015MakeTreeId", "dbo.Christmas2015MakeTree");
            DropForeignKey("dbo.ChildrensDayHiddenPictureSns", "ChildrensDayHiddenPictureId", "dbo.ChildrensDayHiddenPictures");
            DropForeignKey("dbo.BackToSchool2016BingoQuizSns", "BackToSchool2016BingoQuizId", "dbo.BackToSchool2016BingoQuiz");
            DropIndex("dbo.WordEventShares", new[] { "WordEventId" });
            DropIndex("dbo.WordEvents", new[] { "PersonalInfoId" });
            DropIndex("dbo.UserLogins", new[] { "UserId" });
            DropIndex("dbo.UserClaims", new[] { "UserId" });
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropIndex("dbo.NinjaBarbie2016Sharing", new[] { "UserId" });
            DropIndex("dbo.MavelFrozenSNS", new[] { "UserId" });
            DropIndex("dbo.MainStreamSurpriseSNS", new[] { "MainStreamSurpriseId" });
            DropIndex("dbo.FindingDory2017SNS", new[] { "UserId" });
            DropIndex("dbo.Christmas2015MakeTreeSNSShare", new[] { "Christmas2015MakeTreeId" });
            DropIndex("dbo.ChildrensDayHiddenPictureSns", new[] { "ChildrensDayHiddenPictureId" });
            DropIndex("dbo.BackToSchool2016BingoQuizSns", new[] { "BackToSchool2016BingoQuizId" });
            DropTable("dbo.WordWinners");
            DropTable("dbo.WordEventShares");
            DropTable("dbo.WordEvents");
            DropTable("dbo.UserLogins");
            DropTable("dbo.UserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Roles");
            DropTable("dbo.PersonalInfos");
            DropTable("dbo.NinjaBarbie2016User");
            DropTable("dbo.NinjaBarbie2016Sharing");
            DropTable("dbo.MavelFrozenUsers");
            DropTable("dbo.MavelFrozenSNS");
            DropTable("dbo.MainStreamSurpriseSNS");
            DropTable("dbo.MainStreamSurprises");
            DropTable("dbo.MagicKinderAppLaunchings");
            DropTable("dbo.FindingDory2017User");
            DropTable("dbo.FindingDory2017SNS");
            DropTable("dbo.Christmas2015Win");
            DropTable("dbo.Christmas2015WinPrizeSetting");
            DropTable("dbo.Christmas2015MakeTreeSNSShare");
            DropTable("dbo.Christmas2015MakeTree");
            DropTable("dbo.ChildrensDayHiddenPictureSns");
            DropTable("dbo.ChildrensDayHiddenPictures");
            DropTable("dbo.BackToSchool2016BingoQuizSns");
            DropTable("dbo.BackToSchool2016BingoQuiz");
        }
    }
}
