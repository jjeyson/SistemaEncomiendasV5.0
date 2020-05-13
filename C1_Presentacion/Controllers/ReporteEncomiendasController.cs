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
    public class ReporteEncomiendasController : Controller
    {
        EnvioEncomiendaServicio objEnvioEncomienda = new EnvioEncomiendaServicio();
        GestionarCliente objCliente = new GestionarCliente();
        GestionarRuta objRuta = new GestionarRuta();

        public ActionResult ListarReporte()
        {
            //List<Ruta> lstRuta = objRuta.listarRuta();
            //var lsRuta = new SelectList(lstRuta, "idRuta", "Descripcion");
            //ViewBag.ListaRutas = lsRuta;
            return View();
        }

        //    [HttpPost]
          public ActionResult ListarEnvios(String Desde, String Hasta)
        {
            Usuario usuario = (Usuario)Session["usuario"];
            Int32 idSucursal = usuario.sucursal.IdSucursal;
            if (Hasta != "" && Desde != "")
            {
                if (Convert.ToDateTime(Hasta) < Convert.ToDateTime(Desde))
                {
                    ViewBag.mensaje = "Ingrese las fechas Correctas ";
                    return View();
                }

                else
                {
                    List<DocumentoPago> lista = objEnvioEncomienda.ListarReporte(Desde, Hasta, idSucursal);

                    if (lista.Count() > 0)
                    {
                        Session.Remove("MontoTotales");
                        return PartialView(lista);
                    }
                    else
                    {
                        Session.Remove("MontoTotales");
                        ViewBag.mensaje = "No se encontraron envios en ese rango de fechas.";
                        return View();
                    }
                }
            }
            else
            {
                List<DocumentoPago> lista = objEnvioEncomienda.ListarReporte(Desde, Hasta, idSucursal);

                if (lista.Count() > 0)
                {
                    Session.Remove("MontoTotales");
                    return PartialView(lista);
                }
                else
                {
                    Session.Remove("MontoTotales");
                    ViewBag.mensaje = "No se encontraron envios en ese rango de fechas.";
                    return View();
                }
            }
        }


        private void MontoTotalSesion()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Monto", Type.GetType("System.Double"));
            Session["MontoTotales"] = dt;
        }

        public ActionResult ObtenerMontoTotal(String Desde, String Hasta)
        {
            Usuario usuario = (Usuario)Session["usuario"];
            Int32 idSucursal = usuario.sucursal.IdSucursal;
            if (Session["MontoTotales"] == null) { MontoTotalSesion(); }
            DataTable dt = (DataTable)Session["MontoTotales"];
            DocumentoPago documento = objEnvioEncomienda.MontoTotalReporte(Desde, Hasta, idSucursal);
            Boolean YaExiste = false; DataRow r = dt.NewRow();
            foreach (DataRow filas in dt.Rows)
            {
                if (dt.Rows.Count > 0)
                {
                    YaExiste = true;
                    break;
                }
            }
            if (!YaExiste)
            {
                if (documento != null)
                {
                    r["Monto"] = documento.Monto;
                    dt.Rows.Add(r);
                    return PartialView();
                }
                else
                {
                    ViewBag.mensaje = "Ingrese las fechas Correctas."; return PartialView();
                }
            }
            else
            {
                Session.Remove("MontoTotales");
                if (Session["MontoTotales"] == null) { MontoTotalSesion(); }
                DocumentoPago documento2 = objEnvioEncomienda.MontoTotalReporte(Desde, Hasta,idSucursal);
                if (documento != null)
                {
                    r["Monto"] = documento2.Monto;
                    dt.Rows.Add(r);
                    return PartialView();
                }
                else
                {
                    ViewBag.mensaje = "Ingrese las fechas Correctas.";
                    return PartialView();
                }
            }
        }
	}
}