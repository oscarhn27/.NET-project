

using System.Collections.Generic;

namespace Model.DTOs
{
    public class MedicoDTO : UsuarioDTO 
    {
        public virtual string NumColegiado { get; set; }
       
        public virtual ISet<CitaDTO> Citas { get; set; }

        public virtual ISet<PacienteDTO2> Pacientes { get; set; }

        public MedicoDTO() { }
    }
}
