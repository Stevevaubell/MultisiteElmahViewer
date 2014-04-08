using Elmah.Service.Util;
using System;
using System.ServiceProcess;

namespace Elmah.Service
{
    public partial class Service : ServiceBase
    {
        public IErrorHelper ErrorHelper { get; set; }

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

        private void timer_Tick(object sender, System.EventArgs e)
        {
            try
            {
                //Do stuff here!
            }
            catch (Exception error)
            {
                ErrorHelper.LogError(error);
            }
        }
    }
}
