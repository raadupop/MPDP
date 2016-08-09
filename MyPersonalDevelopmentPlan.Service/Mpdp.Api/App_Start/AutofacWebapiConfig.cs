using System.Data.Entity;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Mpdp.Data;
using Mpdp.Data.Infrastructure;
using Mpdp.Data.Repositories;
using Mpdp.Services;
using Mpdp.Services.Abstract;

namespace Mpdp.Api
{
  public class AutofacWebapiConfig
  {
    public static IContainer Contiainer;

    public static void Initialize(HttpConfiguration config)
    {
      Initialize(config, RegisterServices(new ContainerBuilder()));
    }

    public static void Initialize(HttpConfiguration config, IContainer container)
    {
      config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
    }

    private static IContainer RegisterServices(ContainerBuilder builder)
    {
      builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

      // EF MpdpContext
      builder.RegisterType<MpdpContext>().As<DbContext>().InstancePerRequest();

      builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();

      builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();

      builder.RegisterGeneric(typeof (EntityBaseRepository<>)).As(typeof(IEntityBaseRepository<>)).InstancePerDependency();

      // Services 
      builder.RegisterType<EncryptionServices>().As<IEncryptionServices>().InstancePerRequest();

      builder.RegisterType<MembershipServices>().As<IMembershipServices>().InstancePerRequest();

      builder.RegisterType<EffortLoggingServices>().As<IEffortLoggingServices>().InstancePerRequest();

      builder.RegisterType<AnalyticsServices>().As<IAnalyticsServices>().InstancePerDependency();

      Contiainer = builder.Build();
      return Contiainer;
    }
  }
}