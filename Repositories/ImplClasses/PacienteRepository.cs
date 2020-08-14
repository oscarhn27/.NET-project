using Model.Entities;
using NHibernate;
using Repositories.Common;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.ImplClasses
{
    public class PacienteRepository : BaseRepository<Paciente>, IPacienteRepository
    {
        public PacienteRepository(ISession session)
        {
            _session = session;
        }
    }
}
