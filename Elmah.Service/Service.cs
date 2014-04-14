using Elmah.Service.Tasks;
using Elmah.Service.Util;
using System;
using System.ServiceProcess;

namespace Elmah.Service
{
    public partial class Service : ServiceBase
    {
        public IErrorHelper ErrorHelper { get; set; }
        public ICleanUpTask CleanUpTask { get; set; }
        public IFindApplicationsTask FindApplicationsTask { get; set; }

        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }

        public void timer_Tick(object sender, System.EventArgs e)
        {
            try
            {
                FindApplicationsTask.RunTask();
                CleanUpTask.RunTask();
            }
            catch (Exception error)
            {
                ErrorHelper.LogError(error);
            }
        }
    }
}
