[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(KinderJoy.Site.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(KinderJoy.Site.App_Start.NinjectWebCommon), "Stop")]

namespace KinderJoy.Site.App_Start {
    using System;
    using System.Web;
    using System.Web.Http;
    using Domain.Repository.BackToSchool2016;
    using Domain.Repository.ChildrensDay;
    using Domain.Repository.MainStream;
    using Domain.Repository.NinjaBarbie2016;
    using Domain.Service.MainStream;
    using Domain.Service.NinjaBarbie2016;
    using KinderJoy.Domain.Infrastructure;
    using KinderJoy.Domain.Repository.Christmas2015;
    using KinderJoy.Site.Infrastructure;
    using KinderJoy.Site.Infrastructure.Word;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using Domain.Repository.FindingDory2017;
    using Domain.Service.FindingDory2017;
    using Domain.Repository.MagicKinderAppLaunchingEvent;
    using Domain.Service.MagicKinderAppLaunchingEvent;
    using Domain.Repository.MavelFrozen;
    using Domain.Service.MavelFrozen;
    using Domain.Service.KittyJusticeLeague;
    using Domain.Repository.KittyJusticeLeague;
    using Domain.Service.Pororo2018;
    using Domain.Repository.Pororo2018;
    using Domain.Service.DisneyStarWars2018;
    using Domain.RepositoryDisneyStarWars2018;
    using Domain.Repository.DisneyStarWars2018;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel) {
            GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolverForWebApi(kernel);
            kernel.Bind<IAuthenticationManager>().ToMethod(c => System.Web.HttpContext.Current.GetOwinContext().Authentication);
            kernel.Bind<AppUserManager>().ToMethod(c => System.Web.HttpContext.Current.GetOwinContext().GetUserManager<AppUserManager>()).InRequestScope();
            kernel.Bind<AppRoleManager>().ToMethod(c => HttpContext.Current.GetOwinContext().GetUserManager<AppRoleManager>()).InRequestScope();
            kernel.Bind<AppDbContext>().ToSelf().InRequestScope();
            kernel.Bind<IWordService>().To<WordService>().InRequestScope();

            kernel.Bind<ICommonProvider>().To<CommonProvider>().InRequestScope();
            kernel.Bind<IChristmas2015Repository>().To<Christmas2015Repository>().InRequestScope();
            kernel.Bind<IBackToSchool2016Repository>().To<BackToSchool2016Repository>().InRequestScope();
            kernel.Bind<IChildrensDayRepository>().To<ChildrensDayRepository>().InRequestScope();

            kernel.Bind<IMainStreamRepository>().To<MainStreamRepository>().InRequestScope();
            kernel.Bind<IMainStreamService>().To<MainStreamService>().InRequestScope();

            //닌자바비 2016 이벤트
            kernel.Bind<INinjaBarbie2016SharingRepository>().To<NinjaBarbie2016SharingRepository>().InRequestScope();
            kernel.Bind<INinjaBarbie2016UserRepository>().To<NinjaBarbie2016UserRepository>().InRequestScope();
            kernel.Bind<INinjaBarbie2016Service>().To<NinjaBarbie2016Service>().InRequestScope();

            // 2017 도리를찾아서 이벤트
            kernel.Bind<IFindingDory2017UserRepository>().To<FindingDory2017UserRepository>().InRequestScope();
            kernel.Bind<IFindingDory2017SNSRepository>().To<FindingDory2017SNSRepository>().InRequestScope();
            kernel.Bind<IFindingDory2017Service>().To<FindingDory2017Service>().InRequestScope();

            // 매직킨더앱런칭 이벤트
            kernel.Bind<IMagicKinderAppLaunchingRepository>().To<MagicKinderAppLaunchingRepository>().InRequestScope();
            kernel.Bind<IMagicKinderAppLaunchingService>().To<MagicKinderAppLaunchingService>().InRequestScope();

            // 2017 마블프로즌 이벤트
            kernel.Bind<IMavelFrozenUserRepository>().To<MavelFrozenUserRepository>().InRequestScope();
            kernel.Bind<IMavelFrozenSnsRepository>().To<MavelFrozenSnsRepository>().InRequestScope();
            kernel.Bind<IMavelFrozenService>().To<MavelFrozenService>().InRequestScope();

            // 2017 키티&저스티스리그 이벤트
            kernel.Bind<IKittyJusticeLeagueService>().To<KittyJusticeLeagueService>().InRequestScope();
            kernel.Bind<IKittyJusticeLeagueInstantLotteryPrizeSettingRepository>().To<KittyJusticeLeagueInstantLotteryPrizeSettingRepository>().InRequestScope();
            kernel.Bind<IKittyJusticeLeagueInstantLotteryRepository>().To<KittyJusticeLeagueInstantLotteryRepository>().InRequestScope();
            kernel.Bind<ITimeProvider>().To<TimeProvider>().InRequestScope();

            // 2018 뽀로로 이벤트
            kernel.Bind<IPororo2018Service>().To<Pororo2018Service>().InRequestScope();
            kernel.Bind<IPororo2018InstantLotteryPrizeSettingRepository>().To<Pororo2018InstantLotteryPrizeSettingRepository>().InRequestScope();
            kernel.Bind<IPororo2018InstantLotteryRepository>().To<Pororo2018InstantLotteryRepository>().InRequestScope();

            // 2018 디즈니&스타워즈 이벤트
            kernel.Bind<IDisneyStarWars2018Service>().To<DisneyStarWars2018Service>().InRequestScope();
            kernel.Bind<IDisneyStarWars2018InstantLotteryPrizeSettingRepository>().To<DisneyStarWars2018InstantLotteryPrizeSettingRepository>().InRequestScope();
            kernel.Bind<IDisneyStarWars2018InstantLotteryRepository>().To<DisneyStarWars2018InstantLotteryRepository>().InRequestScope();
        }        
    }
}
