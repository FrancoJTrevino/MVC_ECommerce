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
        public ActionResult Index(bool sesion = false)
        {
            ViewBag.Sesion = sesion;
            RedirectToAction("_Header", sesion);
            return View(sesion);
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
        public bool sesionIniciada;
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
                x = "_Header";
                sesionIniciada = true;
            }
            else
            {
                x = "InicioSesion";
                sesionIniciada = false;
            }
            return RedirectToAction(x, new { sesion = true});
        }
        public ActionResult SesionIniciada(bool sesion = false)
        {
            ViewBag.Sesion = sesion;
            return View(sesion);
        }
        public ActionResult _Header(bool sesion)
        {
            ViewBag.Sesion = sesion;
            PartialView(sesion);
            return RedirectToAction("SesionIniciada", new { sesion = sesion });
        }
    }
}