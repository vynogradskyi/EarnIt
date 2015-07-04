using System;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Ninject;
using Ninject;
using Ninject.Extensions.Factory;

namespace EarnIt.Ninja.WebAPI
{
    public class NinjectBootstrapper : NinjectNancyBootstrapper
    {
        protected override void ApplicationStartup(IKernel container, IPipelines pipelines)
        {
            // No registrations should be performed in here, however you may
            // resolve things that are needed during application startup.
        }

        protected override void ConfigureApplicationContainer(IKernel container)
        {

        }

        protected override void ConfigureRequestContainer(IKernel container, NancyContext context)
        {
            // Perform registrations that should have a request lifetime
        }

        protected override void RequestStartup(IKernel container, IPipelines pipelines, NancyContext context)
        {
            // No registrations should be performed in here, however you may
            // resolve things that are needed during request startup.
        }
    }

    public static class KernelExtension
    {
        public static IKernel Register<TAbstract, TConcrete>(this IKernel container) where TConcrete : TAbstract
        {
            container.Bind<TAbstract>().To<TConcrete>();
            return container;
        }

        public static IKernel RegisterAsFactory<TAbstract>(this IKernel container) where TAbstract : class
        {
            container.Bind<TAbstract>().ToFactory();
            return container;
        }

        public static IKernel Register(this IKernel container, Type absrt, Type concrete)
        {
            container.Bind(absrt, concrete);
            return container;
        }
    }
}