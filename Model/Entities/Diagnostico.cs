
using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model.Entities
{
    [Class(Table = "DIAGNOSTICO")]
    public class Diagnostico
    {
        [Id(Name = "IdDiagnostico", Column = "IDDIAGNOSTICO")] 
        [Generator(1, Class = "sequence")]
        [Param(2, Name = "sequence", Content = "GENERADOR_ID_DIAG")]
        public virtual int IdDiagnostico { get; set; }
        [Property(Column = "VALORACIONESPECIALISTA")]
        public virtual string ValoracionEspecialista { get; set; }
        [Property(Column = "ENFERMEDAD")]
        public virtual string Enfermedad { get; set; }
        
        [OneToOne(Name = "Cita", ClassType = typeof(Cita), PropertyRef = "Diagnostico")]
        public virtual Cita Cita { get; set; }

        public Diagnostico()
        {

        }
    }
}
