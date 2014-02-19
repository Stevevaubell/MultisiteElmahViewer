
using Elmah.Core.Models;
using Elmah.Core.Services;
using Elmah.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Elmah.Web.Builders.Impl
{
    public class ErrorLogBuilder : IErrorLogBuilder
    {
        public IDataService DataService { get; set; }

        public ErrorLogViewModel Build(string applicationName, int pageNumber)
        {
            ErrorLogViewModel model = new ErrorLogViewModel();

            Expression<Func<Error, bool>> func = x => x.Application == applicationName;
            IList<Error> errors = DataService.Find(func);
            model.Errors = errors.Skip((pageNumber - 1) * 50).Take(50).ToList();
            model.ApplicationName = applicationName;
            model.PageNumber = pageNumber;
            model.TotalPage = (int)Math.Ceiling(errors.Count / (double)50) + 1; ;
            return model;
        }
    }
}