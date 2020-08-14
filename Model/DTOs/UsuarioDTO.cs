using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model.DTOs
{
    public class UsuarioDTO
    {
        public virtual int IdUsuario { get; set; }
        public virtual string Nombre { get; set; }
        public virtual string Apellidos { get; set; }
        public virtual string User { get; set; }
        public virtual string Clave { get; set; }

        public UsuarioDTO()
        {

        }
    }
}
