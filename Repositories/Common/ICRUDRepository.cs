
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Common
{
    public interface ICRUDRepository<T> where T : class
    {
        #region Create
        void Add(T t);
        void Add(IEnumerable<T> t);

        Task AddAsync(T t);
        Task AddAsync(IEnumerable<T> t);
        #endregion

        #region Read
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
        #endregion

        #region Paged
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
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Expression<Func<T, bool>> predicate = null,
             Expression<Func<T, Object>>[] fetchPaths = null
        );
        #endregion

        #region Update
        void Update(T t);
        void Update(IEnumerable<T> t);
        #endregion

        #region Delete
        /// <summary>
        /// Remove as logic level
        /// </summary>
        /// <param name="t"></param>
        void Remove(T t);

        /// Remove collection as logic level
        void Remove(IEnumerable<T> t);
        #endregion
    }
}
