
using NHibernate;
using System.Threading.Tasks; 

namespace Repositories.UnitOfWork
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly ISession _session;
    private ITransaction _transaction;

        public UnitOfWork(ISession session)
        {
          this._session = session;
        }

        public void BeginTransaction()
        {
            _transaction = _session.BeginTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public async Task CommitAsync()
        {
            await _transaction.CommitAsync();
        }

        public async Task Rollback()
        {
            await _transaction.RollbackAsync();
        }

        public void Flush()
        {
            this._session.Flush();
        }

        public void CloseTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }
    }
}