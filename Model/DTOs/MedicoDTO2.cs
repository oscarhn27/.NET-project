using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTOs
{
    public class MedicoDTO2// : UsuarioDTO
    {
        public virtual int IdUsuario { get; set; }
        public virtual string Nombre { get; set; }
        public virtual string Apellidos { get; set; }
        public virtual string User { get; set; }
        public virtual string Clave { get; set; }
        public virtual string NumColegiado { get; set; }

        public MedicoDTO2() { }
    }
}
