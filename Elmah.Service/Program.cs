using Autofac;
using Elmah.Service.IOC;
using System.Configuration;
using System.ServiceProcess;

namespace Elmah.Service
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            IocSetup setup = new IocSetup();
            var builder = new ContainerBuilder();
            var connectionString =  ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            setup.AddDependencies(builder.Build(), connectionString);

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new Service() 
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
