using System;
using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;

using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using Services.Interceptors;
using Services.ImplClasses;
using Services.Interfaces;

namespace Aquaservice.Common
{
    public static class StartupServiceConfiguration
    {

        public static ContainerBuilder GetProyectoContainerBuilder(IServiceCollection services)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.Populate(services);

            //Registrar aqui todos los tipos a ser interceptados
            containerBuilder.RegisterType<MedicoService>().As<IMedicoService>().EnableInterfaceInterceptors().InterceptedBy(typeof(TransactionalInterceptor));
            containerBuilder.RegisterType<PacienteService>().As<IPacienteService>().EnableInterfaceInterceptors().InterceptedBy(typeof(TransactionalInterceptor));

            var interceptor = new TransactionalInterceptor();
            containerBuilder.RegisterInstance(interceptor);

            return containerBuilder;
        }
    }
}
