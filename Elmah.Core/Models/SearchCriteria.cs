
using System;
using System.Linq.Expressions;
using NHibernate.Criterion;

namespace Elmah.Core.Models
{
    public class SearchCriteria<T> where T : BaseModel
    {
        public Expression<Func<T, bool>> Expression { get; set; }
        public string OrderBy { get; set; }
        public int PageNumber { get; set; }
        public int ResultsPerPage { get; set; }
    }
}

