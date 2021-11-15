using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_ECommerce.Models
{
    public class Producto
    {
        public int ID { get; set; }
        public int ID_Empresa { get; set; }
        public int Stock { get; set; }
        public double PrecioCosto { get; set; }
        public double Margen { get; set; }
        public double PrecioBruto
        {
            get
            {
                return (PrecioCosto + Margen);
            }
            set { }
        }
        public double PrecioNeto
        {
            get
            {
                return PrecioBruto * 1.21;
            }
            set { }
        }
        public double PrecioNetoGet { get; set; }
        public string Nombre { get; set; }
        public string Detalle { get; set; }
        public string TipoComponente { get; set; }
        public string Marca { get; set; }
    }
}