using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using C3_Dominio.Entidades;
using C3_Dominio.Contratos;
using C2_Aplicacion.Procesos;
using C2_Aplicacion.Mantenimientos;
using System.Data;

namespace C1_Presentacion.Controllers
{
    public class RecepcionEncomiendaController : Controller
    {
        EnvioEncomiendaServicio objEnvioEncomienda = new EnvioEncomiendaServicio();
        GestionarCliente objCliente = new GestionarCliente();
        private static int idSucursal;
        public ActionResult GestionarRecepcionEncomienda()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GestionarRecepcionEncomienda(FormCollection frm)
        {
            idSucursal = Convert.ToInt32(frm["txtIdSucursal"]);
            Int32 idCliente = Convert.ToInt32(frm["txtIdClienteEntrega"]);
            Boolean inserto = objEnvioEncomienda.insertarRecepcionEncomienda(idCliente);

            if (inserto)
            {
                ViewBag.mensaje = "Se Registro la Entrega de la Encomienda";
                return View();
            }
            else
            {
                // ViewBag.mensaje = "No se pudo Registrar la Entrega de la Encomienda";
                return View();
            }

        }
        private void BuscarClienteEntregaSesion()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("IdCliente", Type.GetType("System.Int32"));
            dt.Columns.Add("Nombre", Type.GetType("System.String"));
            dt.Columns.Add("RazonSocial", Type.GetType("System.String"));
            dt.Columns.Add("TipoDocumento", Type.GetType("System.String"));
            dt.Columns.Add("NroDocumento", Type.GetType("System.String"));
            dt.Columns.Add("Direccion", Type.GetType("System.String"));
            dt.Columns.Add("Telefono", Type.GetType("System.String"));
            Session["BuscaClienteEntrega"] = dt;
        }
        public ActionResult ListarEntrega(String Documento)
        {
            if (Documento == "")
            {
                Session.Remove("BuscaClienteEntrega");
                ViewBag.mensaje = "Ingrese Numero de Documento DNI ( 8 ) o RUC ( 11 ) para buscar Cliente";
                return View();
            }
            else if (Documento != "" && (Documento.Length == 8 || Documento.Length == 11))
            {
                List<DetalleDocumentoEnvioEncomienda> lista = objEnvioEncomienda.buscarClienteEntregaPendiente(Documento, idSucursal);
                if (lista.Count() > 0)
                {
                    Session.Remove("BuscaClienteEntrega");
                    return PartialView(lista);
                }
                else
                {
                    Session.Remove("BuscaClienteEntrega");
                    ViewBag.mensaje = "No se encontraron registros de envíos de encomienda para el cliente o no existe.";
                    return View();
                }
            }
            else if (Documento.Length != 8 || Documento.Length != 11)
            {
                Session.Remove("BuscaClienteEntrega");
                ViewBag.mensaje = "Ingrese Numero de Documento DNI ( 8 ) o RUC ( 11 ) para buscar Cliente";
                return View();
            }
            else
            {
                Session.Remove("BuscaClienteEntrega");
                return View();
            }
        }
        public ActionResult BuscaClienteEntrega(String ClienteEntrega)
        {
            if (Session["BuscaClienteEntrega"] == null) { BuscarClienteEntregaSesion(); }
            DataTable dt = (DataTable)Session["BuscaClienteEntrega"];
            Cliente cliente = objCliente.buscarClientePorNumDocumento2(ClienteEntrega);
            Boolean YaExiste = false;
            DataRow r = dt.NewRow();
            foreach (DataRow filas in dt.Rows)
            {
                if (dt.Rows.Count > 0)
                {
                    YaExiste = true;
                    break;
                }
            } if (!YaExiste)
            {
                if (cliente != null)
                {
                    r["IdCliente"] = cliente.IdCliente;
                    r["Nombre"] = cliente.NombreCliente + " " + cliente.ApellidoPaternoCliente + " " + cliente.ApellidoMaternoCliente + cliente.RazonSocial;
                    r["RazonSocial"] = cliente.RazonSocial;
                    r["TipoDocumento"] = cliente.TipoDocumento;
                    r["NroDocumento"] = cliente.NumeroDocumento;
                    r["Direccion"] = cliente.Direccion;
                    r["Telefono"] = cliente.Telefono;
                    dt.Rows.Add(r);
                    return PartialView();
                }
                else { return PartialView(); }
            }
            else
            {
                Session.Remove("BuscaClienteEntrega");
                if (Session["BuscaClienteEntrega"] == null) { BuscarClienteEntregaSesion(); }
                Cliente cliente2 = objCliente.buscarClientePorNumDocumento2(ClienteEntrega);
                if (cliente != null)
                {
                    r["IdCliente"] = cliente2.IdCliente;
                    r["Nombre"] = cliente2.NombreCliente + " " + cliente2.ApellidoPaternoCliente + " " + cliente2.ApellidoMaternoCliente + cliente2.RazonSocial;
                    r["RazonSocial"] = cliente2.RazonSocial;
                    r["TipoDocumento"] = cliente2.TipoDocumento;
                    r["NroDocumento"] = cliente2.NumeroDocumento;
                    r["Direccion"] = cliente2.Direccion;
                    r["Telefono"] = cliente2.Telefono;
                    dt.Rows.Add(r);
                    return PartialView();
                }
                else { return PartialView(); }
            }
        }

        //[HttpPost]
        //public ActionResult RegistrarRecepcionEncomienda(FormCollection frm)
        //{
        //    Int32 idCliente = Convert.ToInt32(frm["txtIdClienteEntrega"]);
        //    Boolean inserto = objEnvioEncomienda.insertarRecepcionEncomienda(idCliente);

        //    if (inserto)
        //    {
        //        return RedirectToAction("GestionarRecepcionEncomienda", "RecepcionEncomienda");
        //    }
        //    else
        //    {
        //        return RedirectToAction("GestionarRecepcionEncomienda", "RecepcionEncomienda", new { mensaje = "No se pudo Insertar" });
        //    }

        //}
	}
}