using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_ECommerce.Models
{
    public class Persona
    {
        public int ID { get; set; }
        public int ID_Contacto { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string CUIL { get; set; }
        public string DNI {
            get {
                var dni = CUIL.Substring(2, 8);
                return dni;
            }
            set { }
        }
        public DateTime FechaNac { get; set; }

    }
}