[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Finnq.Promotion.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Finnq.Promotion.App_Start.NinjectWebCommon), "Stop")]

namespace Finnq.Promotion.App_Start {
    using Domain.Infrastructures;
    using Domain.Repositories.RouletteEvent;
    using Domain.Repositories.TmapEvent;
    using Domain.Repositories.TWorldRouletteEvent;
    using Domain.Services.RouletteEvent;
    using Domain.Services.TmapEvent;
    using Domain.Services.TWorldRouletteEvent;
    using Infrastructures;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using System;
    using System.Web;
    using System.Web.Http;

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
            kernel.Bind<IAuthenticationManager>().ToMethod(c => HttpContext.Current.GetOwinContext().Authentication).InRequestScope();
            kernel.Bind<AppUserManager>().ToMethod(c => HttpContext.Current.GetOwinContext().GetUserManager<AppUserManager>()).InRequestScope();
            kernel.Bind<AppRoleManager>().ToMethod(c => HttpContext.Current.GetOwinContext().GetUserManager<AppRoleManager>()).InRequestScope();
            kernel.Bind<AppDbContext>().ToMethod(c => HttpContext.Current.GetOwinContext().Get<AppDbContext>()).InRequestScope();

            GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolverForWebApi(kernel);

            kernel.Bind<ITimeProvider>().To<TimeProvider>().InRequestScope();
            kernel.Bind<ICommonProvider>().To<CommonProvider>().InRequestScope();
            kernel.Bind<ITmapEventEntryRepository>().To<TmapEventEntryRepository>().InRequestScope();
            kernel.Bind<ITmapEventEntryService>().To<TmapEventEntryService>().InRequestScope();

            kernel.Bind<IRouletteEventService>().To<RouletteEventService>().InRequestScope();
            kernel.Bind<IRouletteEventEntryRepository>().To<RouletteEventEntryRepository>().InRequestScope();
            kernel.Bind<IRouletteEventPrizeSettingRepository>().To<RouletteEventPrizeSettingRepository>().InRequestScope();

            kernel.Bind<ITRouletteEventService>().To<TRouletteEventService>().InRequestScope();
            kernel.Bind<ITRouletteEventEntryRepository>().To<TRouletteEventEntryRepository>().InRequestScope();
            kernel.Bind<ITRouletteEventPrizeSettingRepository>().To<TRouletteEventPrizeSettingRepository>().InRequestScope();

            kernel.Bind<ITWorldRouletteEventService>().To<TWorldRouletteEventService>().InRequestScope();
            kernel.Bind<ITWorldRouletteEventEntryRepository>().To<TWorldRouletteEventEntryRepository>().InRequestScope();
            kernel.Bind<ITWorldRouletteEventPrizeSettingRepository>().To<TWorldRouletteEventPrizeSettingRepository>().InRequestScope();
        }        
    }
}
