
using Elmah.Core.Models;
using Elmah.Core.Services;
using Elmah.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Elmah.Service.Tasks.Impl
{
    public class FindApplicationsTask : IFindApplicationsTask
    {
        public IDataService DataService { get; set; }
        public IAppSettingsHelper AppSettingsHelper { get; set; }

        public void RunTask()
        {
            IList<Elmah.Core.Models.Error> distinctApplication = DataService.FindDistinct<Elmah.Core.Models.Error>("Application");
            IList<Application> applications = DataService.GetAll<Application>();

            var result = distinctApplication.Where(er => !applications.Any(app => er.Application == app.Name));

            foreach (var notFound in result)
            {
                Application application = new Application();
                application.Name = notFound.Application;

                DataService.Save(application);
            }
        }
    }
}
