

using System.Collections.Generic;

namespace Model.DTOs
{
    public class MedicoDTO// : UsuarioDTO 
    {
        public virtual int IdUsuario { get; set; }
        public virtual string Nombre { get; set; }
        public virtual string Apellidos { get; set; }
        public virtual string User { get; set; }
        public virtual string Clave { get; set; }
        public virtual string NumColegiado { get; set; }
       
        public virtual ISet<CitaDTO> Citas { get; set; }

        //public virtual ISet<PacienteDTO2> Pacientes { get; set; }

        public MedicoDTO() { }
    }
}
