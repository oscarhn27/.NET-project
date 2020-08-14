using AutoMapper;
using Model.DTOs;
using Model.Entities;
using Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Common
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Medico, MedicoDTO>();
            CreateMap<MedicoDTO, Medico>();
            CreateMap<Medico, MedicoDTO2>();
            CreateMap<MedicoDTO2, Medico>();
            CreateMap<Diagnostico, DiagnosticoDTO>();
            CreateMap<DiagnosticoDTO, Diagnostico>();
            CreateMap<Cita, CitaDTO>();
            CreateMap<CitaDTO, Cita>();
            CreateMap<Paciente, PacienteDTO>();
            CreateMap<PacienteDTO, Paciente>();
            CreateMap<Paciente, PacienteDTO2>();
            CreateMap<PacienteDTO2, Paciente>();
            CreateMap<Usuario, UsuarioDTO>();
            CreateMap<UsuarioDTO, Usuario>();

            CreateMap<DataCollection<Medico>, DataCollection<MedicoDTO>>();
            CreateMap<DataCollection<MedicoDTO>, DataCollection<Medico>>();
            CreateMap<DataCollection<Medico>, DataCollection<MedicoDTO2>>();
            CreateMap<DataCollection<MedicoDTO2>, DataCollection<Medico>>();
            CreateMap<DataCollection<Paciente>, DataCollection<PacienteDTO>>();
            CreateMap<DataCollection<PacienteDTO>, DataCollection<Paciente>>();
            CreateMap<DataCollection<Paciente>, DataCollection<PacienteDTO2>>();
            CreateMap<DataCollection<PacienteDTO2>, DataCollection<Paciente>>();
        }
    }
}
