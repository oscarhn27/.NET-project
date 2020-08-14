
using Autofac.Extras.DynamicProxy;
using AutoMapper;
using Model.DTOs;
using Model.Entities;
using Repositories.Common;
using Repositories.Interfaces;
using Repositories.UnitOfWork;
using Services.Common;
using Services.Interceptors;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Services.ImplClasses
{

    //[Intercept(typeof(TransactionalInterceptor))]
    public class MedicoService : BaseService, IMedicoService
    {

        private readonly IMedicoRepository _medicoRepository;
        private readonly IUnitOfWork _unitOfWork;


        public MedicoService(IMedicoRepository medicoRepository, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _medicoRepository = medicoRepository;
            _unitOfWork = unitOfWork;
        }

        public List<Medico> GetAllMedicos()
        {
            return _medicoRepository.GetAll().ToList();
        }

        public Medico GetMedicoById(int idMedico)
        {
            return _medicoRepository.GetOne(idMedico);
        }

        public void AddMedico(Medico m)
        {
            _medicoRepository.Add(m);
        }

        public void UpdateMedico(Medico m)
        {
            _medicoRepository.Update(m);
        }

        public void DeleteMedicoById(int idMedico)
        {
            Medico m = GetMedicoById(idMedico);
            _medicoRepository.Remove(m);
        }
    }
}