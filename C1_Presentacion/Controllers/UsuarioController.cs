using C2_Aplicacion.Mantenimientos;
using C3_Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace C1_Presentacion.Controllers
{
    public class UsuarioController : Controller
    {
        //
        // GET: /Usuario/
        GestionarUsuario objUsuario = new GestionarUsuario();
        public ActionResult ListaUsuario()
        {
            Usuario usuario = (Usuario)Session["usuario"];
            Int32 idSucursal = usuario.sucursal.IdSucursal;
            List<Usuario> lista = objUsuario.listarUsuario(idSucursal);
            return View(lista);
        }
        [HttpGet]
        public ActionResult UsuarioSave(Int32 idUsuario, FormCollection frm)
        {
            try
            {
                List<TipoUsuario> lisUsuario = new List<TipoUsuario>();
                lisUsuario = objUsuario.listarTipoUsuario();
                ViewBag.ListTipoUsuario = lisUsuario;
                List<Sucursal> listSucursal = new List<Sucursal>();
                listSucursal = objUsuario.Sucursal_GetAll();
                ViewBag.ListSucursal = listSucursal;
                if (idUsuario > 0)
                {
                    Usuario usuario = objUsuario.Usuario_GetByID(idUsuario);

                    return View(usuario);
                }
                else
                {
                    return View();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [HttpPost]
        public ActionResult UsuarioSave(FormCollection frm, Int32 idUSuario)
        {
            try
            {
                Usuario usuario = new Usuario
                {
                    IdUsuario = idUSuario,
                    NombreUsuario = frm["txtNombreUsuario"],
                    ApellidosUsuario = frm["txtApellidos"],
                    DNI = frm["txtDni"],
                    Usuarios = frm["txtUsuario"],
                    Clave = frm["txtClave"],
                    Direccion = frm["txtDireccion"],
                    Telefono = frm["txtTelefono"],
                    Activo = true,
                    sucursal = new Sucursal
                    {
                        IdSucursal = Convert.ToInt32(frm["sucursal.IdSucursal"])
                    },
                    tipoUsuario = new TipoUsuario
                    {
                        id = Convert.ToInt32(frm["tipoUsuario.id"])
                    }
                };
                Int32 modificar = objUsuario.Usuario_Save(usuario);

                if (modificar > 0)
                {
                    ViewBag.mensaje = "El cliente ha sido actualizado.";
                }
                else
                {
                    ViewBag.mensaje = "Ocurrio un error.";
                }
                return RedirectToAction("UsuarioSave", "Usuario", new { idUSuario });
            }
            catch (ApplicationException z)
            {
                ViewBag.mensaje = z.Message;
                return RedirectToAction("UsuarioSave", "Usuario", new
                {
                    idUSuario
                });
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Error", new { error = e.Message });
            }
        }
        public ActionResult UsuarioDelete(Int32 idUsuario)
        {
            Boolean elimino = objUsuario.Usuario_Delete(idUsuario);

            if (elimino)
            {
                return RedirectToAction("ListaUsuario", "Usuario");
            }
            else
            {
                return RedirectToAction("ListaUsuario", "Usuario", new { mensaje = "No se pudo eliminar usuario" });
            }
        }
    }
}