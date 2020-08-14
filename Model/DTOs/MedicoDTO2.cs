using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTOs
{
    public class MedicoDTO2 : UsuarioDTO
    {
        public virtual string NumColegiado { get; set; }

        public MedicoDTO2() { }
    }
}
