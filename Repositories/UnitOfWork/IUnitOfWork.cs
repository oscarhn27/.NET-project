
using NHibernate;
using System;
using System.Threading.Tasks;

namespace Repositories.UnitOfWork
{
  public interface IUnitOfWork
  {
        void BeginTransaction();

        void Commit();
        Task CommitAsync();
        Task Rollback();
        void Flush();
        void CloseTransaction();
    }
}