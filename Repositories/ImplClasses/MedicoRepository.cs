using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Model.Entities;
using Repositories.Interfaces;
using NHibernate;
using Repositories.Common;

namespace Repositories.ImplClasses
{
    public class MedicoRepository : BaseRepository<Medico>, IMedicoRepository
    {
        public MedicoRepository(ISession session)
        {
            _session = session;
        }
    }
}
