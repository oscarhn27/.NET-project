using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTOs
{
    public class PacienteDTO2 : UsuarioDTO
    {
        public virtual string NSS { get; set; }

        public virtual string NumTarjeta { get; set; }

        public virtual string Telefono { get; set; }

        public virtual string Direccion { get; set; }

        public PacienteDTO2() { }
    }
}
