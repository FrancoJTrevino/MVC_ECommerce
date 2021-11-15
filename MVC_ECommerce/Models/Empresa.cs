using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_ECommerce.Models
{
    public class Empresa
    {
        public int ID { get; set; }
        public int ID_Contacto { get; set; }
        public string Nombre { get; set; }
        public string CUIT { get; set; }
    }
}