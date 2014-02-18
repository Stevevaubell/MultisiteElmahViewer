using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Elmah.Core.Services.Impl;
using NHibernate;
using System.Diagnostics.Contracts;
using System.Reflection;
using System.Web;

namespace Elmah.Web.IOC
{
    public class IocSetup
    {
        public void AddDependencies(IContainer context, string connectionString)
        {
            Contract.Requires(context != null);
            Contract.Requires(connectionString != null);

            SessionSetup sessionSetup = new SessionSetup(connectionString);
            var sessionFactory = sessionSetup.GetSessionFactory();

            var builder = new ContainerBuilder();

            builder.RegisterInstance(sessionFactory);


            if (HttpContext.Current != null)
            {
                builder.Register(s => s.Resolve<ISessionFactory>().OpenSession()).InstancePerHttpRequest();
            }
            else
            {
                builder.Register(s => s.Resolve<ISessionFactory>().OpenSession());
            }

            var serviceAssembly = Assembly.GetAssembly(typeof(DataService));
            builder.RegisterAssemblyTypes(serviceAssembly)
                .Where(t => t.Name.EndsWith("Service"))
                .InstancePerApiRequest()
                .AsImplementedInterfaces()
                .PropertiesAutowired();

            var assembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Builder"))
                .InstancePerApiRequest()
                .AsImplementedInterfaces()
                .PropertiesAutowired();
            builder.Update(context);
        }
    }
}