using System;

namespace Model.DTOs
{
    public class CitaDTO
    {
       
        public virtual int IdCita { get; set; }
        public virtual DateTime FechaHora { get; set; }
        public virtual string MotivoCita { get; set; }
        public virtual DiagnosticoDTO Diagnostico { get; set; }
        public virtual int Turno { get; set; }

        public virtual MedicoDTO2 Medico { get; set; }
        public virtual PacienteDTO2 Paciente { get; set; }

        public CitaDTO() { }
    }
}
