[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Samsonite.Mall.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Samsonite.Mall.App_Start.NinjectWebCommon), "Stop")]

namespace Samsonite.Mall.App_Start {
    using Domain.Infrastructures;
    using Domain.Repositories.BagtotheFuture;
    using Domain.Repositories.Christmas2017;
    using Domain.Repositories.OneYearAnniversary;
    using Domain.Repositories.TwoYearAnniversary;
    using Domain.Services.BagtotheFuture;
    using Domain.Services.Christmas2017;
    using Domain.Services.OneYearAnniversary;
    using Domain.Services.TwoYearAnniversary;
    using Infrastructure;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using System;
    using System.Web;
    using System.Web.Http;

    public static class NinjectWebCommon {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop() {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel() {
            var kernel = new StandardKernel();
            try {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            } catch {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel) {

            kernel.Bind<IAuthenticationManager>().ToMethod(c => HttpContext.Current.GetOwinContext().Authentication).InRequestScope();
            kernel.Bind<AppUserManager>().ToMethod(c => HttpContext.Current.GetOwinContext().GetUserManager<AppUserManager>()).InRequestScope();
            kernel.Bind<AppRoleManager>().ToMethod(c => HttpContext.Current.GetOwinContext().GetUserManager<AppRoleManager>()).InRequestScope();
            kernel.Bind<AppDbContext>().ToMethod(c => HttpContext.Current.GetOwinContext().Get<AppDbContext>()).InRequestScope();

            kernel.Bind<ICommonProvider>().To<CommonProvider>().InRequestScope();

            //1주년 프로모션 - 오행시 댓글등록 이벤트 rpeository,service registration
            kernel.Bind<IOneYearAnniversaryRepository>().To<OneYearAnniversaryRepository>().InRequestScope();
            kernel.Bind<IOneYearAnniversaryService>().To<OneYearAnniversaryService>().InRequestScope();

            //백투더퓨처 아이디어 공모전 이벤트 repository, service registration
            kernel.Bind<IBagtotheFutureEntryRepository>().To<BagtotheFutureEntryRepository>().InRequestScope();
            kernel.Bind<IBagtotheFutureSnsUserRepository>().To<BagtotheFutureSnsUserRepository>().InRequestScope();
            kernel.Bind<IBagtotheFutureSnsRepository>().To<BagtotheFutureSnsRepository>().InRequestScope();
            kernel.Bind<IBagtotheFutureService>().To<BagtotheFutureService>().InRequestScope();

            //크리스마스 이벤트
            kernel.Bind<IChristmas2017Service>().To<Christmas2017Service>().InRequestScope();
            kernel.Bind<IChristmas2017EntryRepository>().To<Christmas2017EntryRepository>().InRequestScope();

            kernel.Bind<ITimeProvider>().To<TimeProvider>().InRequestScope();

            //2주년
            kernel.Bind<ITwoYearAnniversaryService>().To<TwoYearAnniversaryService>().InRequestScope(); 
            kernel.Bind<ITwoYearAnniversaryWinCouponRepository>().To<TwoYearAnniversaryWinCouponRepository>().InRequestScope(); 
            kernel.Bind<ITwoYearAnniversaryPrizeSettingRepository>().To<TwoYearAnniversaryPrizeSettingRepository>().InRequestScope(); 
            kernel.Bind<ITwoYearAnniversaryEntryRepository>().To<TwoYearAnniversaryEntryRepository>().InRequestScope();

            GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolverForWebApi(kernel);
        }
    }
}
