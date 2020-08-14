
using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model.Entities
{
    [JoinedSubclass(Table = "Paciente",Extends = "Model.Entities.Usuario")]
    [Key(Column = "IDPACIENTE")]
    public class Paciente : Usuario
    {
        [Property(Column = "NSS")]
        public virtual string NSS { get; set; }
        [Property(Column = "NUMTARJETA")]
        public virtual string NumTarjeta { get; set; }
        [Property(Column = "TELEFONO")]
        public virtual string Telefono { get; set; }
        [Property(Column = "DIRECCION")]
        public virtual string Direccion { get; set; }


        [Set(0, Name ="Citas")]
        [Key(1, Column = "IDPACIENTE")]
        [OneToMany(2, ClassType = typeof(Cita))]
        public virtual ISet<Cita> Citas { get; set; }

        [Set(0, Name = "Medicos", Table = "MEDICO_PACIENTE", Lazy = CollectionLazy.True, Cascade = "save-update", Inverse = true)]
        [Key(1, Column = "IDPACIENTE")]
        [ManyToMany(1, ClassType = typeof(Medico), Column = "IDMEDICO")]
        public virtual ISet<Medico> Medicos { get; set; }

        public Paciente()
        {
            Medicos = new HashSet<Medico>();
            Citas = new HashSet<Cita>();
        }
    }
}
