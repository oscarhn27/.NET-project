
using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model.Entities
{
    [Class(Table = "CITA")]
    public class Cita
    {
        [Id(Name = "IdCita", Column = "IDCITA")]
        [Generator(1, Class = "sequence")]
        [Param(2, Name = "sequence", Content = "GENERADOR_ID_CITA")]
        public virtual int IdCita { get; set; }
        [Property(Column = "FECHAHORA")]
        public virtual DateTime FechaHora { get; set; }
        [Property(Column = "MOTIVOCITA")]
        public virtual string MotivoCita { get; set; }

        //[OneToOne(Name = "Diagnostico", ClassType = typeof(Diagnostico), PropertyRef = "Cita", Cascade = "all")]
        [ManyToOne(Name = "Diagnostico", Column = "IDDIAGNOSTICO" ,ClassType = typeof(Diagnostico), Cascade = "all", Unique = true)]
        public virtual Diagnostico Diagnostico { get; set; }

        [Property(Column = "TURNO")]
        public virtual int Turno { get; set; }

        [ManyToOne(Column = "IDMEDICO", NotNull = true)]
        public virtual Medico Medico { get; set; }

        [ManyToOne(Column = "IDPACIENTE", NotNull = true)]
        public virtual Paciente Paciente { get; set; }

        public Cita()
        {
        }

        public Cita(DateTime FechaHora, string MotivoCita, Diagnostico Diagnostico, Medico Medico, Paciente paciente)
        {
            this.FechaHora = FechaHora;
            this.MotivoCita = MotivoCita;
            this.Diagnostico = Diagnostico;
            this.Medico = Medico;
            this.Paciente = paciente;
        }
    }
}
