using System.Timers;
using Elmah.Core.Util;
using Elmah.Core.Util.Impl;
using Elmah.Service.Tasks;
using Elmah.Service.Util;
using System;
using System.ServiceProcess;

namespace Elmah.Service
{
    public partial class Service : ServiceBase
    {
        public IErrorHelper ErrorHelper { get; set; }
        public IAppSettingsHelper AppSettingsHelper { get; set; }
        public ICleanUpTask CleanUpTask { get; set; }
        public IFindApplicationsTask FindApplicationsTask { get; set; }

        public Service()
        {
            InitializeComponent();
            
        }

        protected override void OnStart(string[] args)
        {
            Timer timer = new Timer(AppSettingsHelper.GetIntConfig(ApplicationSettings.PollingInterval)*1000);
            timer.AutoReset = false;
            timer.Elapsed += timer_Tick;
            timer.Start();
        }

        public void Start()
        {
            OnStart(null);
        }

        protected override void OnStop()
        {
        }

        public void timer_Tick(object sender, System.EventArgs e)
        {
            try
            {
                FindApplicationsTask.RunTask();
                //CleanUpTask.RunTask();
            }
            catch (Exception error)
            {
                ErrorHelper.LogError(error);
            }
        }
    }
}
