using MVC_ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_ECommerce.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult InicioSesion()
        {
            return View();
        }
        public ActionResult CrearCuenta()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CrearCuenta(FormCollection coleccion)
        {
            AdministracionCliente ac = new AdministracionCliente();
            Contacto contacto = new Contacto
            {
                Mail = coleccion["inputMail"],
                Telefono = coleccion["inputTelefono"],
                Direccion = coleccion["inputDireccion"]
            };
            Persona persona = new Persona
            {
                Nombre = coleccion["inputNombre"],
                Apellido = coleccion["inputApellido"],
                CUIL = coleccion["inputCUIL"],
                FechaNac = Convert.ToDateTime(coleccion["inputFechaNacimiento"].ToString())
            };
            Cliente cliente = new Cliente
            {
                FechaReg = DateTime.Now,
                Nombre_Usuario = coleccion["inputUsuario"],
                Password = coleccion["inputContra"]
            };

            ac.AltaCliente(contacto, persona, cliente);

            return RedirectToAction("CuentaCreada");

        }
        public ActionResult CuentaCreada()
        {
            return View();
        }
        [HttpPost]
        public ActionResult IniciarSesion(FormCollection coleccion)
        {
            var ac = new AdministracionCliente();
            string x;
            var usuario = new Cliente
            {
                Nombre_Usuario = coleccion["inputUsuario"],
                Password = coleccion["inputPassword"]
            };

            if(ac.IniciarSesion(usuario) == true)
            {
                x = "SesionIniciada";
            }
            else
            {
                x = "InicioSesion";
            }
            return RedirectToAction(x);
        }
        public ActionResult SesionIniciada()
        {

            return View();
        }
    }
}