
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Common
{
    public interface IReadRepository<T> where T : class
    {
        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int? take = null,
             Expression<Func<T, Object>>[] fetchPaths = null);

        Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int? take = null,
             Expression<Func<T, Object>>[] fetchPaths = null);

        T Single(
            Expression<Func<T, bool>> predicate,
             Expression<Func<T, Object>>[] fetchPaths = null);

        Task<T> SingleAsync(
            Expression<Func<T, bool>> predicate,
             Expression<Func<T, Object>>[] fetchPaths = null);

        T SingleOrDefault(
            Expression<Func<T, bool>> predicate,
             Expression<Func<T, Object>>[] fetchPaths = null);

        Task<T> SingleOrDefaultAsync(
            Expression<Func<T, bool>> predicate,
             Expression<Func<T, Object>>[] fetchPaths = null);

        T First(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
             Expression<Func<T, Object>>[] fetchPaths = null);

        Task<T> FirstAsync(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
             Expression<Func<T, Object>>[] fetchPaths = null);

        T FirstOrDefault(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
             Expression<Func<T, Object>>[] fetchPaths = null);

        Task<T> FirstOrDefaultAsync(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
             Expression<Func<T, Object>>[] fetchPaths = null);

        // Extras
        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);
        int Count(Expression<Func<T, bool>> predicate = null);

        Task<decimal?> SumAsync(Expression<Func<T, bool>> predicate = null);
        decimal? Sum(Expression<Func<T, bool>> predicate = null);

        bool Exist(int id);
        Task<bool> ExistAsync(int id);

        T GetOne(int id);
        Task<T> GetOneAsync(int id);
    }
}
