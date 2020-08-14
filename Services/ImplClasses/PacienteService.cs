using Model.Entities;
using Repositories.Interfaces;
using Repositories.UnitOfWork;
using Services.Common;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.ImplClasses
{
    public class PacienteService : BaseService, IPacienteService
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PacienteService(IPacienteRepository pacienteRepository, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _pacienteRepository = pacienteRepository;
            _unitOfWork = unitOfWork;
        }

        public void AddPaciente(Paciente p)
        {
            _pacienteRepository.Add(p);
        }

        public void DeletePacienteById(int idPaciente)
        {
            _pacienteRepository.Remove(GetPacienteById(idPaciente));
        }

        public List<Paciente> GetAllPaciente()
        {
            return _pacienteRepository.GetAll().ToList();
        }

        public Paciente GetPacienteById(int idPaciente)
        {
            return _pacienteRepository.GetOne(idPaciente);
        }

        public void UpdatePaciente(Paciente p)
        {
            _pacienteRepository.Update(p);
        }
    }
}
