using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_ECommerce.Models
{
    public class Contacto
    {
        public int ID { get; set; }
        public string Mail { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
    }
}