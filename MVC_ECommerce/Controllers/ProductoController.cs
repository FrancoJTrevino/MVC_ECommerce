using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_ECommerce.Models;

namespace MVC_ECommerce.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListaProductos()
        {
            return View();
        }
        public ActionResult CrearProducto()
        {

            return View();
        }
        [HttpPost]
        public ActionResult CrearProducto(FormCollection coleccion)
        {
            var ap = new AdministracionProducto();
            var selectEmp = coleccion["selectEmpresa"];
            var contacto = new Contacto();
            var empresa = new Empresa();
            var producto = new Producto();

            if(selectEmp == "CrearNueva")
            {
                contacto.Mail = coleccion["inputMail"];
                contacto.Telefono = coleccion["inputTelefono"];
                contacto.Direccion = coleccion["inputDireccion"];

                empresa.Nombre = coleccion["inputNombreEmp"];
                empresa.CUIT = coleccion["inputCUIT"];
            }
            else
            {
                empresa.Nombre = selectEmp;
            }

            producto.Stock = Convert.ToInt32(coleccion["inputStock"]);
            producto.PrecioCosto = Convert.ToDouble(coleccion["inputPrecioCosto"]);
            producto.Margen = Convert.ToDouble(coleccion["inputMargen"]);
            producto.Nombre = coleccion["inputNombre"];
            producto.Detalle = coleccion["inputDetalles"];
            producto.TipoComponente = coleccion["inputTipo"];
            producto.Marca = coleccion["inputMarca"];

            ap.AltaProducto(contacto, empresa, producto);
            return RedirectToAction("ProductoCreado");
        }
        public ActionResult ProductoCreado()
        {
            return View();
        }
    }
}