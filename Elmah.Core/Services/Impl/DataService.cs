using Elmah.Core.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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
            return _session.QueryOver<T>().Where(x => x.ErrorId == id).List().FirstOrDefault();
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

        public void Delete<T>(Guid id) where T : BaseModel
        {
            T model = _session.QueryOver<T>().Where(x => x.ErrorId == id).SingleOrDefault();
                _session.Delete(model);
            

            _session.Flush();
        }
    }
}
