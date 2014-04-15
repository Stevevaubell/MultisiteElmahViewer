using System;
using System.Threading;
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
            var connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            var builder = setup.AddDependencies(connectionString);

            using (var container = builder.Build())
            {
                if (!Environment.UserInteractive)
                {
                    ServiceBase[] ServicesToRun;
                    ServicesToRun = new ServiceBase[]
                        {
                            container.Resolve<Service>()
                        };
                    ServiceBase.Run(ServicesToRun);
                }
                else
                {
                    Service service = container.Resolve<Service>(); 
                    service.Start();
                    Thread.Sleep(30000);
                }
            }
        }
    }
}
