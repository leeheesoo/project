[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(INGLife.Event.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(INGLife.Event.App_Start.NinjectWebCommon), "Stop")]

namespace INGLife.Event.App_Start
{
    using Domain.Infrastructures;
    using Domain.Repositories.KidsNote;
    using Domain.Repositories.Managements;
    using Domain.Repositories.MarketingAgree;
    using Domain.Repositories.Rebranding;
    using Domain.Services.KidsNote;
    using Domain.Services.Managements;
    using Domain.Services.MarketingAgree;
    using Domain.Services.Nilririmambo;
    using Domain.Services.Rebranding;
    using Domain.Services.OverFortyFiveDb;
    using Infrastructure;
    using Infrastructures.GoogleAnalyticsReportingServices;
    using Infrastructures.Interfaces;
    using Infrastructures.KMCServices;
    using Message.Services;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using System;
    using System.Web;
    using System.Web.Http;
    using Domain.Services.FinancialConcertMarketingAgree;
    using Domain.Repositories.FinancialConcertMarketingAgree;
    using Domain.Services.TumblerEvent;
    using Domain.Services.FinancialConsultantSharing;
    using Domain.Repositories.FinancialConsultantSharing;

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
            GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolverForWebApi(kernel);
            kernel.Bind<ITimeProvider>().To<TimeProvider>().InRequestScope();
            kernel.Bind<ICommonProvider>().To<CommonProvider>().InRequestScope();

            kernel.Bind<IAuthenticationManager>().ToMethod(c => HttpContext.Current.GetOwinContext().Authentication);
            kernel.Bind<AppUserManager>().ToMethod(c => HttpContext.Current.GetOwinContext().GetUserManager<AppUserManager>()).InRequestScope();
            kernel.Bind<AppRoleManager>().ToMethod(c => HttpContext.Current.GetOwinContext().GetUserManager<AppRoleManager>()).InRequestScope();
            kernel.Bind<AppDbContext>().ToMethod(x => HttpContext.Current.GetOwinContext().Get<AppDbContext>()).InRequestScope();

            kernel.Bind<IEventManagementRepository>().To<EventManagementRepository>().InRequestScope();
            kernel.Bind<IEventManagementService>().To<EventManagementService>().InRequestScope();

            kernel.Bind<IKMCService>().To<KMCService>().InRequestScope();

            kernel.Bind<IGoogleAnalyticsReportingService>().To<GoogleAnalyticsReportingService>().InRequestScope();
            kernel.Bind<IAffiliatesRepository>().To<AffiliatesRepository>().InRequestScope();
            kernel.Bind<IAffiliatesService>().To<AffiliatesService>().InRequestScope();            

            //kernel.Bind<SmsDbContext>().ToSelf().InRequestScope();
            //kernel.Bind<ISmsRepository>().To<SmsRepository>();
            //kernel.Bind<ISmsService>().To<SmsService>();
            kernel.Bind<IUplusSmsApiService>().To<UplusSmsApiService>();

            kernel.Bind<IKidsNoteEntryRepository>().To<KidsNoteEntryRepository>().InRequestScope();
            kernel.Bind<IKidsNoteService>().To<KidsNoteService>().InRequestScope();

            kernel.Bind<INilririmamboEntryRepository>().To<NilririmamboEntryRepository>().InRequestScope();
            kernel.Bind<INilririmamboService>().To<NilririmamboService>().InRequestScope();

            kernel.Bind<IMarketingAgreeEntryRepository>().To<MarketingAgreeEntryRepository>().InRequestScope();
            kernel.Bind<IMarketingAgreeService>().To<MarketingAgreeService>().InRequestScope();

            kernel.Bind<IOverFortyFiveDbEntryRepository>().To<OverFortyFiveDbRepository>().InRequestScope();
            kernel.Bind<IOverFortyFiveService>().To<OverFortyFiveService>().InRequestScope();

            kernel.Bind<IRebrandingMarketingAgreeEntryRepository>().To<RebrandingMarketingAgreeEntryRepository>().InRequestScope();
            kernel.Bind<IRebrandingConsultingAgreeEntryRepository>().To<RebrandingConsultingAgreeEntryRepository>().InRequestScope();
            kernel.Bind<IRebrandingEventService>().To<RebrandingEventService>().InRequestScope();

            kernel.Bind<IFinancialConcertMarketingAgreeRepository>().To<FinancialConcertMarketingAgreeRepository>().InRequestScope();
            kernel.Bind<IFinancialConcertMarketingAgreeService>().To<FinancialConcertMarketingAgreeService>().InRequestScope();

            kernel.Bind<ITumblerEventRepository>().To<TumblerEventRepository>().InRequestScope();
            kernel.Bind<ITumblerEventService>().To<TumblerEventService>().InRequestScope();

            kernel.Bind<IFinancialConsultantRepository>().To<FinancialConsultantRepository>().InRequestScope();
            kernel.Bind<IFinancialConsultantOriginCustomerRepository>().To<FinancialConsultantOriginCustomerRepository>().InRequestScope();
            kernel.Bind<IFinancialConsultantNewCustomerRepository>().To<FinancialConsultantNewCustomerRepository>().InRequestScope();
            kernel.Bind<IFinancialConsultantSharingService>().To<FinancialConsultantSharingService>().InRequestScope();
            
        }
    }
}
