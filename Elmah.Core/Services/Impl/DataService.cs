using Elmah.Core.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate.Criterion;

namespace Elmah.Core.Services.Impl
{
    public class DataService : IDataService
    {
        protected readonly ISession _session;

        public DataService(ISession session)
        {
            _session = session;
        }

        public T Get<T>(Guid id) where T : BaseModel
        {
            return _session.QueryOver<T>().Where(x => x.Id == id).List().FirstOrDefault();
        }

        public IList<T> GetAll<T>() where T : BaseModel
        {
            IList<T> items = _session.QueryOver<T>().List();
            return items ?? new List<T>();
        }

        public IList<T> Find<T>(Expression<Func<T, bool>> expression) where T : BaseModel
        {
            return _session.QueryOver<T>().Where(expression).List<T>();
        }

        public SearchResult<T> Find<T>(SearchCriteria<T> criteria) where T : BaseModel
        {
            var query = _session.QueryOver<T>()
                .Where(criteria.Expression);

            if (criteria != null && criteria.Expression != null)
                query.Where(criteria.Expression);

            if (criteria != null && criteria.OrderBy != null)
                query.UnderlyingCriteria.AddOrder(Order.Desc(criteria.OrderBy));

            IList<T> results = query.Skip(criteria.PageNumber * criteria.ResultsPerPage)
            .Take(criteria.ResultsPerPage)
            .List<T>();

            SearchResult<T> result = new SearchResult<T>();
            result.ResultsList = results;
            result.TotalResults = _session.QueryOver<T>().Where(criteria.Expression).RowCount();

            return result;
        }

        public void Delete<T>(Guid id) where T : BaseModel
        {
            T model = _session.QueryOver<T>().Where(x => x.Id == id).SingleOrDefault();
            _session.Delete(model);


            _session.Flush();
        }

        public void Save<T>(T model) where T : BaseModel
        {
            using (var tx = _session.BeginTransaction())
            {
                _session.SaveOrUpdate(model);
                tx.Commit();
                _session.Flush();
            }
        }
    }
}
