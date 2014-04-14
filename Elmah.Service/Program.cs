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
            try
            {
                IocSetup setup = new IocSetup();
                var builder = new ContainerBuilder();
                var connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
                setup.AddDependencies(builder.Build(), connectionString);
            }
            catch (Exception error)
            {
                var errorLog =
                new SqlErrorLog(System.Configuration.ConfigurationManager.ConnectionStrings["db"].ConnectionString)
                {
                    ApplicationName = "Application Health Service"
                };
                errorLog.Log(new Error(error));
            }

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new Service() 
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
