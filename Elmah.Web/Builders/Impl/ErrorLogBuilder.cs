
using Elmah.Core.Models;
using Elmah.Core.Services;
using Elmah.Web.Models;
using System;
using System.Linq.Expressions;

namespace Elmah.Web.Builders.Impl
{
    public class ErrorLogBuilder : IErrorLogBuilder
    {
        public const int ResultsPerPage = 50;

        public IDataService DataService { get; set; }

        public ErrorLogViewModel Build(string applicationName, int pageNumber)
        {
            ErrorLogViewModel model = new ErrorLogViewModel();

            Expression<Func<Error, bool>> func = x => x.Application == applicationName;
            SearchCriteria<Error> criteria = new SearchCriteria<Error>();
            criteria.OrderBy = "TimeUtc";
            criteria.ResultsPerPage = ResultsPerPage;
            criteria.PageNumber = pageNumber - 1;
            criteria.Expression = func;

            SearchResult<Error> result = DataService.Find<Error>(criteria);
            model.Errors = result.ResultsList;
            model.ApplicationName = applicationName;
            model.PageNumber = pageNumber;
            model.TotalPage = result.TotalResults > ResultsPerPage 
                ? (int)Math.Ceiling(result.TotalResults / (double)ResultsPerPage)
                : 1;
            return model;
        }
    }
}