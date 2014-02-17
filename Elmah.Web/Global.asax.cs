using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Elmah.Web.IOC;
using Microsoft.AspNet.SignalR;

namespace Elmah.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes); 
            SetupAutoFac();
        }

        private static void SetupAutoFac()
        {
            var dependencyBuilder = new IocSetup();

            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly())
                   .InstancePerApiRequest()
                   .PropertiesAutowired();
            builder.RegisterModule(new AutofacWebTypesModule());
            builder.RegisterSource(new ViewRegistrationSource());

            var container = builder.Build();

            var signalrResolver = new Autofac.Integration.SignalR.AutofacDependencyResolver(container);
            GlobalHost.DependencyResolver = signalrResolver;

            dependencyBuilder.AddDependencies(container, ConfigurationManager.ConnectionStrings["db"].ConnectionString);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}