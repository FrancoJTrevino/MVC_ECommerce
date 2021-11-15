using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_ECommerce.Models
{
    public class Cliente
    {
        public int ID { get; set; }
        public int ID_Persona { get; set; }
        public DateTime FechaReg { get; set; }
        public string Nombre_Usuario { get; set; }
        public string Password { get; set; }
    }
}