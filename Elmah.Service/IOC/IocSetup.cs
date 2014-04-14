
using Autofac;
using Elmah.Core.Services.Impl;
using Elmah.Core.Util;
using NHibernate;
using System.Reflection;

namespace Elmah.Service.IOC
{
    public class IocSetup
    {
        public void AddDependencies(IContainer context, string connectionString)
        {
            var builder = new Autofac.ContainerBuilder();

            SessionSetup sessionSetup = new SessionSetup(connectionString);
            var sessionFactory = sessionSetup.GetSessionFactory();
            builder.Register(s => s.Resolve<ISessionFactory>().OpenSession());


            var serviceAssembly = Assembly.GetAssembly(typeof (DataService));
            builder.RegisterAssemblyTypes(serviceAssembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .PropertiesAutowired();

            builder.RegisterAssemblyTypes(serviceAssembly)
                .Where(t => t.Name.EndsWith("Helper"))
                .AsImplementedInterfaces()
                .PropertiesAutowired();

            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Helper"))
                .AsImplementedInterfaces()
                .PropertiesAutowired();

            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Task"))
                .AsImplementedInterfaces()
                .PropertiesAutowired();


            builder.RegisterType<Service>();

            builder.RegisterInstance(sessionFactory);

            builder.Update(context);
        }
    }
}
