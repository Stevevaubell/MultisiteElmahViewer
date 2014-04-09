
using Elmah.Core.Models;
using Elmah.Core.Services;
using Elmah.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Elmah.Service.Tasks.Impl
{
    public class CleanUpTask : ICleanUpTask
    {
        public IDataService DataService { get; set; }
        public IAppSettingsHelper AppSettingsHelper { get; set; }

        public void RunTask()
        {
            IList<ElmahSettings> settings = DataService.GetAll<ElmahSettings>();
            IList<Application> applications = DataService.GetAll<Application>();

            foreach (Application application in applications)
            {
                var setting = settings.FirstOrDefault(x => x.Application.Id == application.Id);

                if (setting == null)
                {
                    setting = new ElmahSettings();
                    setting.Application = application;
                    setting.LengthToKeepResults =
                        AppSettingsHelper.GetIntConfig(ApplicationSettings.DefaultLengthToKeepResults);
                    DataService.Save(setting);
                }

                Expression<Func<Core.Models.Error, bool>> func =
                    x =>
                        x.TimeUtc <=
                        new DateTime(DateTime.UtcNow.Year,
                            DateTime.UtcNow.Month,
                            DateTime.UtcNow.Day).AddDays(-1 * setting.LengthToKeepResults);
                IList<Core.Models.Error> errors = DataService.Find(func);

                foreach (var error in errors)
                {
                    DataService.Delete<Core.Models.Error>(error.Id);
                }
            }
        }
    }
}
