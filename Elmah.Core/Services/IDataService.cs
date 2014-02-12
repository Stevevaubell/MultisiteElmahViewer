
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Elmah.Core.Models;

namespace Elmah.Core.Services
{
    public interface IDataService
    {
        T Get<T>(Guid id) where T : BaseModel;
        IList<T> GetAll<T>() where T : BaseModel;
        IList<T> Find<T>(Expression<Func<T, bool>> expression) where T : BaseModel;
        void Delete<T>(Guid id) where T : BaseModel;
    }
}
