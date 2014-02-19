
using Elmah.Core.Models;
using Elmah.Core.Services;
using Elmah.Web.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Elmah.Web.Builders.Api.Impl
{
    public class ApplicationStateApiBuilder : IApplicationStateApiBuilder
    {
        public IDataService DataService { get; set; }

        public IEnumerable<ApplicationState> Build()
        {
            IList<Application> list = DataService.GetAll<Application>();

            Expression<Func<Error, bool>> func = x => x.TimeUtc <= new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day) &&
                x.TimeUtc >= new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 23, 59, 59);
            IList<Error> errors = DataService.Find(func);

            IList<ApplicationState> states = new List<ApplicationState>();
            foreach (var application in list)
            {
                ApplicationState state = new ApplicationState();
                state.Application = application.Name;
                state.ErrorCount = errors.Count(x => x.Application == application.Name);
                states.Add(state);
            }

            return states;
        }
    }
}