using Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces
{
    public interface IPacienteService
    {
        List<Paciente> GetAllPaciente();
        Paciente GetPacienteById(int idPaciente);
        void AddPaciente(Paciente p);
    }
}
