using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using C3_Dominio.Entidades;
using C3_Dominio.Contratos;
using C2_Aplicacion.Mantenimientos;

namespace C1_Presentacion.Controllers
{
    public class ClienteController : Controller
    {
        GestionarCliente objGestionarCliente = new GestionarCliente();
        GestionarTipoDocumento objGestionarTipoDocumento = new GestionarTipoDocumento();
        List<TipoDocumento> lstTipoDocumento = new List<TipoDocumento>();
        public ActionResult RegistroCliente()
        {
            List<Cliente> lista = objGestionarCliente.listarCliente();
            return View(lista);
        }
        [HttpPost]
        public ActionResult RegistroCliente(FormCollection frm, String numDocumento)
        {
            numDocumento = frm["txtDniRuc"];
            if (numDocumento == "")
            {
                List<Cliente> lista = objGestionarCliente.listarCliente();
                ViewBag.mensaje = "Ingrese Numero de Documento DNI o RUC para buscar Cliente";
                return View(lista);
            }
            else if (numDocumento != "" && (numDocumento.Length == 8 || numDocumento.Length == 11))
            {
                List<Cliente> lista = objGestionarCliente.buscarClientePorNumDocumento(numDocumento);
                if (lista.Count > 0)
                {
                    return View(lista);
                }
                else
                {
                    List<Cliente> lista2 = objGestionarCliente.listarCliente();
                    ViewBag.mensaje = "No existe un cliente con ese numero de DNI o RUC";
                    return View(lista2);
                }

            }
            else if (numDocumento.Length != 8 || numDocumento.Length != 11)
            {
                List<Cliente> lista = objGestionarCliente.listarCliente();
                ViewBag.mensaje = "Ingrese Numero de Documento DNI ( 8 ) o RUC ( 11 ) para buscar Cliente";
                return View(lista);
            }
            else
            {
                return View();
            }
        }
        //public ViewResult NuevoCliente( Cliente c)
        //{
        //    return View(c);
        //}
        public ViewResult NuevoCliente()
        {
            return View();
        }
        //http://aspnetmvc.wikispaces.com/Html.DropDownList


        [HttpPost]
        public ActionResult NuevoCliente(FormCollection frm, string combo)
        {
            try
            {
                Cliente cliente = new Cliente
                {
                    NombreCliente = frm["txtNombre"],
                    ApellidoPaternoCliente = frm["txtApellidoPaterno"],
                    ApellidoMaternoCliente = frm["txtApellidoMaterno"],
                    // cliente.NumeroDocumento = frm["txtNroDocumento"];
                    Direccion = frm["txtDireccion"],
                    Telefono = frm["txttelefono"],
                    RazonSocial = frm["txtRazonSocial"],
                    TipoDocumento = combo
                };

                //Validacion Datos Vacios de Campos
                int contador = 0;
                if (combo == "DNI")
                {
                    cliente.RazonSocial = "";
                    if (cliente.NombreCliente.Equals("") || cliente.NombreCliente == string.Empty)
                    {
                        ViewBag.mensaje = "Se debe de ingresar Nombre Cliente";
                        contador++;
                        return View();
                    }
                    if (cliente.ApellidoPaternoCliente.Equals("") || cliente.ApellidoPaternoCliente == string.Empty)
                    {
                        ViewBag.mensaje = "Se debe de ingresar Apellido Paterno Cliente";
                        contador++;
                        return View();
                    }
                    if (cliente.ApellidoMaternoCliente.Equals("") || cliente.ApellidoMaternoCliente == string.Empty)
                    {
                        ViewBag.mensaje = "Se debe de ingresar Apellido Materno Cliente";
                        contador++;
                        return View();
                    }
                    if (frm["txtNroDocumento"].Length != 8 && cliente.TipoDocumento.Equals(frm["txtNroDocumento"]))
                    {
                        ViewBag.mensaje = "Si seleccionó DNI entonces son 8 digitos";
                        contador++;
                        return View();
                    }
                    else
                    {
                        cliente.NumeroDocumento = frm["txtNroDocumento"];

                    }
                }
                else if (combo != "DNI")
                {
                    cliente.NombreCliente = "";
                    cliente.ApellidoPaternoCliente = "";
                    cliente.ApellidoMaternoCliente = "";
                    if (cliente.RazonSocial.Equals("") || cliente.RazonSocial == string.Empty)
                    {
                        ViewBag.mensaje = "Se debe de ingresar Nombre Cliente";
                        contador++;
                        return View();
                    }
                    if (frm["txtNroDocumento"].Length == 11)
                    {
                        cliente.NumeroDocumento = frm["txtNroDocumento"];
                    }
                    else
                    {
                        ViewBag.mensaje = "Si seleccionó RUC entonces son 11 digitos";
                        contador++;
                        return View();
                    }
                }



                if (contador > 1)
                {

                    ViewBag.mensaje = "Se debe de ingresar los datos Cliente";

                    return View();
                }

                //Fin Validacion datos vacios de campos

                int inserto = objGestionarCliente.insertarCliente(cliente);

                if (inserto <= 0)
                {
                    switch (inserto)
                    {
                        case 0: ViewBag.mensaje = "El cliente ha sido registrado.";
                            return View();
                        case -1: ViewBag.mensaje = "Ya existe un cliente con el mismo numero de Documento";
                            return View();

                        case -2: ViewBag.mensaje = "Ya existe un cliente con misma Razon Social";
                            return View();

                    }

                }

                else
                {
                    ViewBag.mensaje = "No se pudo Insertar.";
                    return View();
                    //return RedirectToAction("NuevoCliente", "Cliente", new { mensaje = "No se pudo Insertar" });
                }
                return View();
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Error", new { error = e.Message });
            }
        }

        public ActionResult modificarCliente(int idCliente, FormCollection frm)
        {
            try
            {
                Cliente cliente = objGestionarCliente.devolverCliente(idCliente);
                TipoDocumento tipoDocumento = new TipoDocumento();
                return View(cliente);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        [HttpPost]
        public ActionResult modificarCliente(FormCollection frm, int idCliente, Cliente cliente, String combo)
        {
            try
            {
                cliente.NombreCliente = frm["txtNombre"];
                cliente.ApellidoPaternoCliente = frm["txtApellidoPaterno"];
                cliente.ApellidoMaternoCliente = frm["txtApellidoMaterno"];

                cliente.Direccion = frm["txtDireccion"];
                cliente.Telefono = frm["txttelefono"];
                cliente.RazonSocial = frm["txtRazonSocial"];
                cliente.TipoDocumento = combo;
                //Validacion Datos Vacios de Campos
                int contador = 0;
                if (combo == "DNI")
                {
                    cliente.RazonSocial = "";
                    if (cliente.NombreCliente.Equals("") || cliente.NombreCliente == string.Empty)
                    {
                        ViewBag.mensaje = "Se debe de ingresar Nombre Cliente";
                        contador++;
                        return View();
                    }
                    if (cliente.ApellidoPaternoCliente.Equals("") || cliente.ApellidoPaternoCliente == string.Empty)
                    {
                        ViewBag.mensaje = "Se debe de ingresar Apellido Paterno Cliente";
                        contador++;
                        return View();
                    }
                    if (cliente.ApellidoMaternoCliente.Equals("") || cliente.ApellidoMaternoCliente == string.Empty)
                    {
                        ViewBag.mensaje = "Se debe de ingresar Apellido Materno Cliente";
                        contador++;
                        return View();
                    }
                    frm["txtNroDocumento"] = frm["txtNroDocumento"].Replace(" ", "");
                    if (frm["txtNroDocumento"].Length == 8)
                    {
                        cliente.NumeroDocumento = frm["txtNroDocumento"];
                    }
                    else
                    {
                        ViewBag.mensaje = "Si seleccionó DNI entonces son 8 digitos";
                        contador++;
                        return View();
                    }
                }
                else if (combo != "DNI")
                {
                    cliente.NombreCliente = "";
                    cliente.ApellidoPaternoCliente = "";
                    cliente.ApellidoMaternoCliente = "";
                    if (cliente.RazonSocial.Equals("") || cliente.RazonSocial == string.Empty)
                    {
                        ViewBag.mensaje = "Se debe de ingresar Nombre Cliente";
                        contador++;
                        return View();
                    }
                    if (frm["txtNroDocumento"].Length == 11)
                    {
                        cliente.NumeroDocumento = frm["txtNroDocumento"];
                    }
                    else
                    {
                        ViewBag.mensaje = "Si seleccionó RUC entonces son 11 digitos";
                        contador++;
                        return View();
                    }
                }
                if (contador > 1)
                {
                    ViewBag.mensaje = "Se debe de ingresar Numero Documento Cliente";
                    return View();
                }

                //Fin Validacion datos vacios de campos

                Boolean modificar = objGestionarCliente.modificarCliente(idCliente, cliente);

                if (modificar)
                {
                    ViewBag.mensaje = "El cliente ha sido actualizado.";
                    return View();
                }
                else
                {
                    return RedirectToAction("modificarCliente", "Cliente", new { mensaje = "No se pudo modificar Cliente" });
                }
            }
            catch (ApplicationException z)
            {
                return RedirectToAction("modificarCliente", "Cliente", new { mensaje = z.Message });
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Error", new { error = e.Message });
            }
        }

        public ActionResult eliminarCliente(int idCliente)
        {
            Boolean elimino = objGestionarCliente.EliminarCliente(idCliente);

            if (elimino)
            {
                return RedirectToAction("RegistroCliente", "Cliente");
            }
            else
            {
                return RedirectToAction("RegistroCliente", "Cliente", new { mensaje = "No se pudo eliminar Cliente" });
            }
        }

	}
}