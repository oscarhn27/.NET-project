using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model.Entities
{
    [Class(Table = "Usuario")]
    public class Usuario
    {
        [Id(Name = "IdUsuario", Column ="IDUSUARIO")]
        [Generator(1, Class = "sequence")]
        [Param(2, Name = "sequence", Content = "GENERADOR_ID_USER")]
        public virtual int IdUsuario { get; set; }

        [Property(Column = "NOMBRE")]
        public virtual string Nombre { get; set; }
        [Property(Column = "APELLIDOS")]
        public virtual string Apellidos { get; set; }
        [Property(Column = "USUARIO")]
        public virtual string User { get; set; }
        [Property(Column = "CLAVE")]
        public virtual string Clave { get; set; }

        public Usuario()
        {

        }

        public Usuario(string nombre, string apellidos, string usuario, string clave)
        {
            this.Nombre = nombre;
            this.Apellidos = apellidos;
            this.User = usuario;
            this.Clave = clave;
        }
    }
}
