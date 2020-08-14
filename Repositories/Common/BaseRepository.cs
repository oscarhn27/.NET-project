using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace Repositories.Common
{
    public class BaseRepository<T> where T : class
    {

        protected ISession _session;

        protected IQueryable<T> PrepareQuery(
            IQueryable<T> query,
            Expression<Func<T, bool>> predicate = null,
            Expression<Func<T, Object>>[] fetchPaths = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int? take = null
        )
        {
            if (fetchPaths != null)
            {
                foreach (var fetchPath in fetchPaths)
                {
                    query = query.Fetch(fetchPath);
                }
            }

            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                query = orderBy(query);

            if (take.HasValue)
                query = query.Take(Convert.ToInt32(take));

            return query;
        }

        #region Extras
        public virtual async Task<decimal?> SumAsync(
            Expression<Func<T, bool>> predicate = null
        )
        {
            var query = _session.Query<T>().AsQueryable();

            query = PrepareQuery(query, predicate);

            return await ((IQueryable<decimal?>)query).SumAsync();
        }

        public virtual decimal? Sum(
            Expression<Func<T, bool>> predicate = null
        )
        {
            var query = _session.Query<T>().AsQueryable();

            query = PrepareQuery(query, predicate);

            return ((IQueryable<decimal?>)query).Sum();
        }

        public virtual async Task<int> CountAsync(
            Expression<Func<T, bool>> predicate = null
        )
        {
            var query = _session.Query<T>().AsQueryable();

            query = PrepareQuery(query, predicate);

            return await query.CountAsync();
        }

        public virtual int Count(
            Expression<Func<T, bool>> predicate = null
        )
        {
            var query = _session.Query<T>().AsQueryable();

            query = PrepareQuery(query, predicate);

            return query.Count();
        }
        #endregion

        #region Get All
        public virtual IEnumerable<T> GetAll(
                    Expression<Func<T, bool>> predicate = null,
                    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                    int? take = null,
                    Expression<Func<T, Object>>[] fetchPaths = null
                )
        {
            var query = _session.Query<T>().AsQueryable();

            query = PrepareQuery(query, predicate, fetchPaths, orderBy, take);

            return query.ToList();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int? take = null,
             Expression<Func<T, Object>>[] fetchPaths = null
        )
        {
            var query = _session.Query<T>().AsQueryable();

            query = PrepareQuery(query, predicate, fetchPaths, orderBy, take);

            return await query.ToListAsync();
        }
        #endregion

        #region GetOne
        public T GetOne(int id)
        {
            return _session.Get<T>(id);
        }

        public async Task<T> GetOneAsync(int id)
        {
            return await _session.GetAsync<T>(id);
        }
        #endregion

        #region Paged
        public virtual async Task<DataCollection<T>> GetPagedAsync(
            int page,
            int take,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            Expression<Func<T, bool>> predicate = null,
            Expression<Func<T, Object>>[] fetchPaths = null
        )
        {
            var query = _session.Query<T>().AsQueryable();
            var originalPages = page;

            page--;

            if (page > 0)
                page = page * take;

            query = PrepareQuery(query, predicate, fetchPaths, orderBy);

            var result = new DataCollection<T>
            {
                Items = await query.Skip(page).Take(take).ToListAsync(),
                Total = await query.CountAsync(),
                Page = originalPages
            };

            if (result.Total > 0)
            {
                result.Pages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(result.Total) / take));
            }

            return result;
        }

        public virtual DataCollection<T> GetPaged(
            int page,
            int take,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            Expression<Func<T, bool>> predicate = null,
            Expression<Func<T, Object>>[] fetchPaths = null
        )
        {
            var query = _session.Query<T>().AsQueryable();
            var originalPages = page;

            page--;

            if (page > 0)
                page = page * take;

            query = PrepareQuery(query, predicate, fetchPaths, orderBy);

            var result = new DataCollection<T>
            {
                Items = query.Skip(page).Take(take).ToList(),
                Total = query.Count(),
                Page = originalPages
            };

            if (result.Total > 0)
            {
                result.Pages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(result.Total) / take));
            }

            return result;
        }
        #endregion

        #region First
        public virtual T First(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Expression<Func<T, Object>>[] fetchPaths = null
        )
        {
            var query = _session.Query<T>().AsQueryable();

            query = PrepareQuery(query, predicate, fetchPaths, orderBy);

            return query.First();
        }

        public virtual async Task<T> FirstAsync(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Expression<Func<T, Object>>[] fetchPaths = null
        )
        {
            var query = _session.Query<T>().AsQueryable();

            query = PrepareQuery(query, predicate, fetchPaths, orderBy);

            return await query.FirstAsync();
        }

        public virtual T FirstOrDefault(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Expression<Func<T, Object>>[] fetchPaths = null
        )
        {
            var query = _session.Query<T>().AsQueryable();

            query = PrepareQuery(query, predicate, fetchPaths, orderBy);

            return query.FirstOrDefault();
        }

        public virtual async Task<T> FirstOrDefaultAsync(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Expression<Func<T, Object>>[] fetchPaths = null
        )
        {
            var query = _session.Query<T>().AsQueryable();

            query = PrepareQuery(query, predicate, fetchPaths, orderBy);

            return await query.FirstOrDefaultAsync();
        }
        #endregion

        #region Single
        public virtual T Single(
            Expression<Func<T, bool>> predicate,
            Expression<Func<T, Object>>[] fetchPaths = null
        )
        {
            var query = _session.Query<T>().AsQueryable();

            query = PrepareQuery(query, predicate, fetchPaths);

            return query.Single();
        }

        public virtual async Task<T> SingleAsync(
            Expression<Func<T, bool>> predicate,
             Expression<Func<T, Object>>[] fetchPaths = null
        )
        {
            var query = _session.Query<T>().AsQueryable();

            query = PrepareQuery(query, predicate, fetchPaths);

            return await query.SingleAsync();
        }

        public virtual T SingleOrDefault(
            Expression<Func<T, bool>> predicate,
             Expression<Func<T, Object>>[] fetchPaths = null
        )
        {
            var query = _session.Query<T>().AsQueryable();

            query = PrepareQuery(query, predicate, fetchPaths);

            return query.SingleOrDefault();
        }

        public virtual async Task<T> SingleOrDefaultAsync(
            Expression<Func<T, bool>> predicate,
             Expression<Func<T, Object>>[] fetchPaths = null
        )
        {
            var query = _session.Query<T>().AsQueryable();

            query = PrepareQuery(query, predicate, fetchPaths);

            return await query.SingleOrDefaultAsync();
        }
        #endregion

        #region Add
        public virtual void Add(T t)
        {
            _session.Save(t);
        }

        public virtual void Add(IEnumerable<T> t)
        {
            _session.Save(t);
        }

        public virtual async Task AddAsync(T t)
        {
            await _session.SaveAsync(t);
        }

        public virtual async Task AddAsync(IEnumerable<T> t)
        {
            await _session.SaveAsync(t);
        }
        #endregion

        #region Remove
        public virtual void Remove(T t)
        {
            _session.Delete(t);
        }

        public virtual void Remove(IEnumerable<T> t)
        {
            _session.Delete(t);
        }
        #endregion

        #region Update
        public virtual void Update(T t)
        {
            _session.Update(t);
        }

        public virtual void Update(IEnumerable<T> t)
        {
            _session.Update(t);
        }
        #endregion

        #region Exist
        public virtual bool Exist(int id)
        {
            return GetOne(id) != null;
        }

        public virtual async Task<bool> ExistAsync(int id)
        {
            return await GetOneAsync(id) != null;
        }
        #endregion
    }
}