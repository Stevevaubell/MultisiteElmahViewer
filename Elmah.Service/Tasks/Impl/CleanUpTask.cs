
using Elmah.Core.Models;
using Elmah.Core.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace Elmah.Service.Tasks.Impl
{
    public class CleanUpTask : ICleanUpTask
    {
        public IDataService DataService { get; set; }

        public void RunTask()
        {
            IList<ElmahSettings> settings = DataService.GetAll<ElmahSettings>();

            if (settings == null || settings.Count == 0)
                throw new InvalidDataException("No settings for application");

            Expression<Func<Core.Models.Error, bool>> func =
                x =>
                    x.TimeUtc <=
                    new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day).AddDays(-1 *
                                                                                                           settings
                                                                                                           .FirstOrDefault()
                                                                                                               .LengthToKeepResults);
            IList<Core.Models.Error> errors = DataService.Find(func);

            foreach (var error in errors)
            {
                DataService.Delete<Core.Models.Error>(error.ErrorId);
            }
        }
    }
}
