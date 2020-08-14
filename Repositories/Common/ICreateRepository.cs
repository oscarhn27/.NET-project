using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Common
{
    public interface ICreateRepository<T> where T : class
    {
        void Add(T t);
        void Add(IEnumerable<T> t);

        Task AddAsync(T t);
        Task AddAsync(IEnumerable<T> t);
    }
}
