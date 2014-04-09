using Elmah.Core.Models;
using Elmah.Core.Services;
using Elmah.Web.Models;
using Elmah.Web.Util;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Elmah.Web.Builders.Impl
{
    public class ErrorDisplayBuilder : IErrorDisplayBuilder
    {
        public const int ResultsPerPage = 50;

        public IDataService DataService { get; set; }
        public IErrorXmlHelper ErrorXmlHelper { get; set; }

        public ErrorDisplayViewModel Build(string errorId)
        {
            ErrorDisplayViewModel model = new ErrorDisplayViewModel();

            Expression<Func<Error, bool>> func = x => x.Id == new Guid(errorId);
            SearchCriteria<Error> criteria = new SearchCriteria<Error>();
            criteria.OrderBy = "TimeUtc";
            criteria.ResultsPerPage = ResultsPerPage;
            criteria.PageNumber = 0;
            criteria.Expression = func;
            
            SearchResult<Error> result = DataService.Find<Error>(criteria);
            model.Error = result.ResultsList.FirstOrDefault();
            model.OriginalError = model.Error != null
                ? ErrorXmlHelper.DecodeString(model.Error.AllXml)
                : null;

            return model;
        }
    }
}