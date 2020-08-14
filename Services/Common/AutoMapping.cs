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
            CreateMap<Diagnostico, DiagnosticoDTO>();
            CreateMap<DiagnosticoDTO, Diagnostico>();
            CreateMap<Cita, CitaDTO>();
            CreateMap<CitaDTO, Cita>();
            CreateMap<Paciente, PacienteDTO>();
            CreateMap<PacienteDTO, Paciente>();
            CreateMap<Usuario, UsuarioDTO>();
            CreateMap<UsuarioDTO, Usuario>();

            CreateMap<DataCollection<Medico>, DataCollection<MedicoDTO>>();
            CreateMap<DataCollection<MedicoDTO>, DataCollection<Medico>>();
        }
    }
}
