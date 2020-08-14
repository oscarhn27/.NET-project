using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aquaservice.Common
{
    /// <summary>
    /// Modela una respuesta hacia el front
    /// </summary>
    public class ProyectoResponse
    {

        /// <summary>
        /// Indica el estado de la respuesta. 0 si es todo OK, 1 para las alertas y 2 para los errores
        /// </summary>
        public byte Status { get; set; }

        /// <summary>
        /// Indica el mensaje a mostrar en el front cuando se lanza una alerta o error
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Contiene el objeto que se desea enviar como respuesta
        /// </summary>
        public object Data { get; set; }


        /// <summary>
        /// Contiene un obejeto que se desea enviar en la respuesta pero que no es 
        /// el contenido principal
        /// </summary>
        public object MetaData { get; set; }
    }
}
