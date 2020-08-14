using Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoRepositories.Base
{
    public interface IPagedRepository<T> where T : class
    {
        Task<DataCollection<T>> GetPagedAsync(
            int page,
            int take,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            Expression<Func<T, bool>> predicate = null,
             Expression<Func<T, Object>>[] fetchPaths = null
        );

        DataCollection<T> GetPaged(
            int page,
            int take,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            Expression<Func<T, bool>> predicate = null,
             Expression<Func<T, Object>>[] fetchPaths = null
        );
    }
}
