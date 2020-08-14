using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTOs
{
    public class PacienteDTO2// : UsuarioDTO
    {
        public virtual int IdUsuario { get; set; }
        public virtual string Nombre { get; set; }
        public virtual string Apellidos { get; set; }
        public virtual string User { get; set; }
        public virtual string Clave { get; set; }
        public virtual string NSS { get; set; }

        public virtual string NumTarjeta { get; set; }

        public virtual string Telefono { get; set; }

        public virtual string Direccion { get; set; }

        public PacienteDTO2() { }
    }
}
