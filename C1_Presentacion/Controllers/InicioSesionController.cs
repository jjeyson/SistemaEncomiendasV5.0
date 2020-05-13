using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using C3_Dominio.Entidades;
using C2_Aplicacion.Mantenimientos;

namespace C1_Presentacion.Controllers
{
    public class InicioSesionController : Controller
    {
        GestionarUsuario gestionaUsuario = new GestionarUsuario();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            try
            {
                string usuarios = form["txtUsuario"];
                string clave = form["txtClave"];
                if (usuarios.Equals("") || usuarios == string.Empty)
                {
                    ViewBag.mensaje = "Se debe ingresar su nombre de usuario";
                    return View();
                }
                if (clave.Equals("") || clave == string.Empty)
                {
                    ViewBag.mensaje = "Se debe ingresar su nombre de clave";
                    return View();
                }
                Usuario usuario = gestionaUsuario.inicioSesion(usuarios, clave);
                
                if (usuario != null)
                {
                    TipoUsuario tu = new TipoUsuario
                    {
                        id = usuario.tipoUsuario.id,
                        nombre = usuario.tipoUsuario.nombre
                    };
                    usuario.tipoUsuario = tu;
                    Session["usuario"] = usuario;
                    Session["tipoUsuario"] = tu;
                    return RedirectToAction("Home", "InicioSesion");
                }
                else
                {

                    ViewBag.mensaje = "Usuario o Password no valido!!!!";
                    return View();
                }
            }
            catch (ApplicationException z)
            {
                return RedirectToAction("Login", "InicioSesion", new { mensaje = z.Message });
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Error", new { error = e.Message });
            }
        }
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult CerrarSesion()
        {
            Session.Remove("usuario");
            return RedirectToAction("Login", "InicioSesion");
        }
	}
}