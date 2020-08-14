
using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model.DTOs
{
    public class PacienteDTO : UsuarioDTO
    {

        public virtual string NSS { get; set; }
        
        public virtual string NumTarjeta { get; set; }
       
        public virtual string Telefono { get; set; }
        
        public virtual string Direccion { get; set; }

        public virtual ISet<MedicoDTO2> Medicos { get; set; }
        public virtual ISet<CitaDTO> Citas { get; set; }


        public PacienteDTO(){ }
     
    }
}
