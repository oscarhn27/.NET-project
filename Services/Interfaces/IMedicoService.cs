
using Autofac.Extras.DynamicProxy;
using Model.DTOs;
using Model.Entities;
using Services.Common;
using Services.Interceptors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    //[Intercept(typeof(TransactionalInterceptor))]
    public interface IMedicoService
    {
        List<Medico> GetAllMedicos();
        Medico GetMedicoById(int idMedico);
        void AddMedico(Medico m);
    }
}