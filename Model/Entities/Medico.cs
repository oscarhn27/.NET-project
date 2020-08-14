
using NHibernate.Mapping;
using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model.Entities
{
    [JoinedSubclass(Table = "MEDICO" , Extends = "Model.Entities.Usuario")]
    [Key(Column = "IDMEDICO")]
    public class Medico : Usuario
    {

                
        [Property(Column = "NUMCOLEGIADO")]
        public virtual string NumColegiado { get; set; }

        [Set(0, Name = "Citas")]
        [Key(1, Column = "IDMEDICO")]
        [OneToMany(2, ClassType = typeof(Cita))]
        public virtual ISet<Cita> Citas { get; set; }

 
        [Set(0, Name = "Pacientes", Table = "MEDICO_PACIENTE", Lazy = CollectionLazy.True, Cascade = "save-update")]
        [Key(1, Column = "IDMEDICO")]
        [ManyToMany(1, ClassType = typeof(Paciente), Column = "IDPACIENTE")]
        public virtual ISet<Paciente> Pacientes { get; set; }

        public Medico() : base()
        {
            Pacientes = new HashSet<Paciente>();
            Citas = new HashSet<Cita>();
        }
        public Medico(string nombre, string apellidos, string usuario, string clave, string numColegiado)
            : base(nombre, apellidos, usuario, clave)
        {
            NumColegiado = numColegiado;
            Pacientes = new HashSet<Paciente>();
            Citas = new HashSet<Cita>();
        }
    }
}
