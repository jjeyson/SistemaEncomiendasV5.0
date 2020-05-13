using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using C3_Dominio.Entidades;
using C3_Dominio.Contratos;
using C2_Aplicacion.Procesos;
using C2_Aplicacion.Mantenimientos;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace C1_Presentacion.Controllers
{
    public class EnvioEncomiendaController : Controller
    {
        GestionarCliente objGestionarCliente = new GestionarCliente();
        EnvioEncomiendaServicio objEnvioEncomienda = new EnvioEncomiendaServicio();
        GestionarRuta objGestionaRuta = new GestionarRuta();
        DetalleDocumentoEnvioEncomienda objDetalleEnvio = new DetalleDocumentoEnvioEncomienda();
        //private double calcularPrecio;
        //private static double subtotal;
        //private static double descuento;
        //private static double montoTotal;
        //private static int idSucursal;

        //SESSION -- VISTAS PARCIALES
        #region Sesion/VistaParciales
        private void CrearTipoDocumentoenSesion()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("NombreTipo", Type.GetType("System.String"));
            dt.Columns.Add("NumSerie", Type.GetType("System.String"));
            dt.Columns.Add("NumDocumento", Type.GetType("System.String"));
            Session["TipoDocumento"] = dt;
        }
        public ActionResult TipoDocumento(String TipoDocumento)
        {
            if (Session["TipoDocumento"] == null) { CrearTipoDocumentoenSesion(); }
            DataTable dt = (DataTable)Session["TipoDocumento"];
            DocumentoPago documento = objEnvioEncomienda.ObtenerNumeros(TipoDocumento, /*idSucursal*/1);
            Boolean YaExiste = false;
            DataRow r = dt.NewRow();
            foreach (DataRow filas in dt.Rows)
            {
                if (dt.Rows.Count > 0)
                {
                    YaExiste = true;
                    break;
                }
            }
            if (YaExiste && TipoDocumento == "--Seleccionar--")
            {
                Session.Remove("TipoDocumento");
                if (Session["TipoDocumento"] == null) { CrearTipoDocumentoenSesion(); }
                DocumentoPago documento2 = objEnvioEncomienda.ObtenerNumeros(TipoDocumento, /*idSucursal*/1);
                DataRow r2 = dt.NewRow();
                r2["NombreTipo"] = TipoDocumento;
                r2["NumSerie"] = "000";
                r2["NumDocumento"] = "0000000";
                dt.Rows.Add(r2);
                return PartialView();
            }
            else if (YaExiste)
            {
                Session.Remove("TipoDocumento");
                if (Session["TipoDocumento"] == null) { CrearTipoDocumentoenSesion(); }
                DocumentoPago documento2 = objEnvioEncomienda.ObtenerNumeros(TipoDocumento, /*idSucursal*/1);
                DataRow r2 = dt.NewRow();
                r2["NombreTipo"] = TipoDocumento + " DE ENVÍO";
                r2["NumSerie"] = documento2.NumSerie;
                r2["NumDocumento"] = documento2.NumDocumento;
                dt.Rows.Add(r2);

                return PartialView();
            }
            else if (!YaExiste && TipoDocumento == "--Seleccionar--")
            {
                Session.Remove("TipoDocumento");
                if (Session["TipoDocumento"] == null) { CrearTipoDocumentoenSesion(); }
                DocumentoPago documento2 = objEnvioEncomienda.ObtenerNumeros(TipoDocumento, /*idSucursal*/1);
                DataRow r2 = dt.NewRow();
                r2["NombreTipo"] = TipoDocumento;
                r2["NumSerie"] = "000";
                r2["NumDocumento"] = "0000000";
                dt.Rows.Add(r2);
                return PartialView();
            }
            else
            {

                r["NombreTipo"] = TipoDocumento + " DE ENVÍO";
                r["NumSerie"] = documento.NumSerie;
                r["NumDocumento"] = documento.NumDocumento;
                dt.Rows.Add(r); //Session.Remove("TipoDocumento");
                return PartialView();
            }
        }


        private void CrearDetalleEnvioenSesion()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Descripcion", Type.GetType("System.String"));
            dt.Columns.Add("Peso", Type.GetType("System.Int32"));
            dt.Columns.Add("CostoGramo", Type.GetType("System.Double"));
            dt.Columns.Add("Precio", Type.GetType("System.Double"));
            Session["DetEnvio"] = dt;
        }

        public ActionResult DetalleEnvio(String Descripcion, Int32 Peso, Int32 idRuta)
        {
            if (Session["DetEnvio"] == null) { CrearDetalleEnvioenSesion(); }
            DataTable dt = (DataTable)Session["DetEnvio"];
            Tarifa tarifa = objGestionaRuta.listarTarifaIdRuta(idRuta);
            Boolean YaExiste = false;
            //   subtotal = 0;
            //if (Session["DetEnvio"] == null) { CrearDetalleEnvioenSesion(); }
            //DataTable dt = (DataTable)Session["DetEnvio"];
            //dt.Rows().Delete();
            //return View();
            foreach (DataRow filas in dt.Rows)
            {
                if (Descripcion == Convert.ToString(filas["Descripcion"]))
                {
                    YaExiste = true;
                    break;
                }
            }
            if (!YaExiste)
            {
                //DataRow r = dt.NewRow();
                //objDetalleEnvio.Peso = Peso;
                //objDetalleEnvio.PrecioGramo = tarifa.PrecioGramo;
                //calcularPrecio = objDetalleEnvio.CalcularPrecio();
                //r["Descripcion"] = Descripcion;
                //r["Peso"] = Peso;
                //r["CostoGramo"] = tarifa.PrecioGramo;
                //r["Precio"] = calcularPrecio;
                //subtotal = objDetalleEnvio.CalcularTotal();
                //  dt.Rows.Add(r);
                return PartialView();
            }
            else
            {
                return PartialView();
                // return RedirectToAction("GestionarEnvioEncomienda", "EnvioEncomienda", new { mensaje = "El producto ingresado Ya Existe" });
            }
        }
        private void BuscarClienteEnvioSesion()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("IdCliente", Type.GetType("System.Int32"));
            dt.Columns.Add("Nombre", Type.GetType("System.String"));
            dt.Columns.Add("RazonSocial", Type.GetType("System.String"));
            dt.Columns.Add("TipoDocumento", Type.GetType("System.String"));
            Session["ClienteEnvio"] = dt;
        }
        public ActionResult ClienteEnvio(String Remitente)
        {
            if (Session["ClienteEnvio"] == null) { BuscarClienteEnvioSesion(); }
            DataTable dt = (DataTable)Session["ClienteEnvio"];
            Cliente cliente = objGestionarCliente.buscarClientePorNumDocumento2(Remitente);
            Boolean YaExiste = false;
            DataRow r = dt.NewRow();
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
                if (cliente != null)
                {
                    r["IdCliente"] = cliente.IdCliente;
                    r["Nombre"] = cliente.NombreCliente + " " + cliente.ApellidoPaternoCliente + " " + cliente.ApellidoMaternoCliente + cliente.RazonSocial;
                    r["RazonSocial"] = cliente.RazonSocial;
                    r["TipoDocumento"] = cliente.TipoDocumento;
                    dt.Rows.Add(r);
                    return PartialView();
                }
                else
                {
                    r["IdCliente"] = 0;
                    r["Nombre"] = "--NO REGISTRADO--";
                    r["RazonSocial"] = "";
                    r["TipoDocumento"] = "";
                    dt.Rows.Add(r);
                    return PartialView();
                }
            }
            else
            {
                Session.Remove("ClienteEnvio");
                if (Session["ClienteEnvio"] == null) { BuscarClienteEnvioSesion(); }
                Cliente cliente2 = objGestionarCliente.buscarClientePorNumDocumento2(Remitente);
                if (cliente != null)
                {
                    r["IdCliente"] = cliente2.IdCliente;
                    r["Nombre"] = cliente2.NombreCliente + " " + cliente2.ApellidoPaternoCliente + " " + cliente2.ApellidoMaternoCliente + cliente2.RazonSocial;
                    r["RazonSocial"] = cliente2.RazonSocial;
                    r["TipoDocumento"] = cliente2.TipoDocumento;
                    dt.Rows.Add(r);
                    return PartialView();
                }
                else
                {
                    r["IdCliente"] = 0;
                    r["Nombre"] = "--NO REGISTRADO--";
                    r["RazonSocial"] = "";
                    r["TipoDocumento"] = "";
                    dt.Rows.Add(r);
                    return PartialView();
                }
            }
        }
        private void BuscarClienteEntregaSesion()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("IdCliente", Type.GetType("System.Int32"));
            dt.Columns.Add("Nombre", Type.GetType("System.String"));
            dt.Columns.Add("Direccion", Type.GetType("System.String"));
            dt.Columns.Add("TipoDocumento", Type.GetType("System.String"));
            Session["ClienteEntrega"] = dt;
        }
        public ActionResult ClienteEntrega(String Destinatario)
        {
            if (Session["ClienteEntrega"] == null) { BuscarClienteEntregaSesion(); }
            DataTable dt = (DataTable)Session["ClienteEntrega"];
            DataRow r = dt.NewRow();
            Cliente cliente = objGestionarCliente.buscarClientePorNumDocumento2(Destinatario);
            Boolean YaExiste = false;
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
                if (cliente != null)
                {
                    r["IdCliente"] = cliente.IdCliente;
                    r["Nombre"] = cliente.NombreCliente + " " + cliente.ApellidoPaternoCliente + " " + cliente.ApellidoMaternoCliente + cliente.ApellidoMaternoCliente + cliente.RazonSocial;
                    r["Direccion"] = cliente.Direccion;
                    r["TipoDocumento"] = cliente.TipoDocumento;
                    dt.Rows.Add(r);
                    return PartialView();
                }
                else
                {
                    r["IdCliente"] = 0;
                    r["Nombre"] = "--NO REGISTRADO--";
                    r["Direccion"] = "";
                    r["TipoDocumento"] = "";
                    dt.Rows.Add(r);
                    return PartialView();
                }
            }
            else
            {
                Session.Remove("ClienteEntrega");
                if (Session["ClienteEntrega"] == null) { BuscarClienteEntregaSesion(); }
                DataTable dt2 = (DataTable)Session["ClienteEntrega"];
                Cliente cliente2 = objGestionarCliente.buscarClientePorNumDocumento2(Destinatario);
                if (cliente != null)
                {
                    r["IdCliente"] = cliente2.IdCliente;
                    r["Nombre"] = cliente2.NombreCliente + " " + cliente2.ApellidoPaternoCliente + " " + cliente2.ApellidoMaternoCliente + cliente2.ApellidoMaternoCliente + cliente2.RazonSocial;
                    r["Direccion"] = cliente2.Direccion;
                    r["TipoDocumento"] = cliente2.TipoDocumento;
                    dt.Rows.Add(r);
                    return PartialView();
                }
                else
                {
                    r["IdCliente"] = 0;
                    r["Nombre"] = "--NO REGISTRADO--";
                    r["Direccion"] = "";
                    r["TipoDocumento"] = "";
                    dt.Rows.Add(r);
                    return PartialView();
                }
            }
        }
        private void CrearMontosenSesion()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Total", Type.GetType("System.Double"));
            dt.Columns.Add("Descuento", Type.GetType("System.Double"));
            dt.Columns.Add("MontoTotal", Type.GetType("System.Double"));
            Session["CalcularPrecios"] = dt;
        }
        public ActionResult CalculoPrecios()
        {
            if (Session["CalcularPrecios"] == null) { CrearMontosenSesion(); }
            DataTable dt = (DataTable)Session["CalcularPrecios"];
            Boolean YaExiste = false;

            DataRow r = dt.NewRow();
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
                //r["Total"] = subtotal;
                //descuento = objDetalleEnvio.CalcularDescuento();
                //r["Descuento"] = descuento;
                //montoTotal = objDetalleEnvio.calcularMontoTotalPagar();
                //r["MontoTotal"] = montoTotal;
                dt.Rows.Add(r);
                return PartialView();
            }
            else
            {
                //Session.Remove("CalcularPrecios");
                //if (Session["CalcularPrecios"] == null) { CrearMontosenSesion(); }
                //r["Total"] = subtotal;
                //descuento = objDetalleEnvio.CalcularDescuento();
                //r["Descuento"] = descuento;
                //montoTotal = objDetalleEnvio.calcularMontoTotalPagar();
                //r["MontoTotal"] = montoTotal;
                dt.Rows.Add(r);
                return PartialView();
            }
        }
        #endregion Sesion/VistaParciales
        //FIN SESSIONES-- VISTAS PARCIALES
        [HttpPost]
        public ActionResult GestionarEnvioEncomienda(FormCollection frm, String combo, String aDomicilio)
        {
            //subtotal = 0;
            //documentoPago.MontoTotal = 0;
            Session.Remove("TipoDocumento");
            Session.Remove("DetEnvio");
            Session.Remove("ClienteEntrega");
            Session.Remove("ClienteEnvio");
            Session.Remove("CalcularPrecios");
            Int32 idSucursal = Convert.ToInt32(frm["txtIdSucursal"]);
            List<Ruta> lstRuta = objGestionaRuta.listarRutaId(idSucursal);
            var lsRuta = new SelectList(lstRuta, "idRuta", "Descripcion");
            ViewBag.ListaRutas = lsRuta;
            return View();

        }
        [HttpGet]
        public ActionResult GestionarEnvioEncomienda()
        {
            Session.Remove("TipoDocumento");
            Session.Remove("DetEnvio");
            Session.Remove("ClienteEntrega");
            Session.Remove("ClienteEnvio");
            Session.Remove("CalcularPrecios");
            Usuario usuario = (Usuario)Session["usuario"];
            Int32 idSucursal = usuario.sucursal.IdSucursal;
            List<Ruta> lstRuta = objGestionaRuta.listarRutaId(idSucursal);
            var lsRuta = new SelectList(lstRuta, "idRuta", "Descripcion");
            ViewBag.ListaRutas = lsRuta;

            return View();
        }
        public ActionResult GestionarEnvioEncomienda(String error)
        {
            ViewBag.mensaje = error;
            return View();

        }
        public ActionResult BorrarItem()
        {
            if (Session["DetEnvio"] == null) { CrearDetalleEnvioenSesion(); }
            DataTable dt = (DataTable)Session["DetEnvio"];
            foreach (DataRow f in dt.Rows)
            {
                f.Delete();
                break;

            }
            return RedirectToAction("DetalleEnvio", "EnvioEncomienda");
        }
        public ActionResult GuardarEnvioEncomienda(FormCollection frm, String combo, String aDomicilio)
        {
            try
            {
                int contador = 0;
                // REGISTRAR DOCUMENTO PAGO
                DocumentoPago documentoPago = new DocumentoPago
                {
                    NumSerie = frm["txtNSerie"],
                    NumDocumento = frm["txtNdocumento"],
                    TipoDocumento = combo
                };
                //documentoPago.Monto = montoTotal;

                //VALIDACIONES CAMPOS
                if (documentoPago.Monto.Equals("") || documentoPago.Monto == 0.0)
                {
                    contador++;
                    return RedirectToAction("GestionarEnvioEncomienda", "EnvioEncomienda", new { error = "Se debe de ingresar Paquetes a Enviar" });
                }
                if (documentoPago.NumSerie.Equals("") || documentoPago.NumSerie == string.Empty)
                {
                    contador++;
                    return RedirectToAction("GestionarEnvioEncomienda", "EnvioEncomienda", new { error = "Se debe de ingresar Numero de Serie" });
                }
                if (documentoPago.NumDocumento.Equals("") || documentoPago.NumDocumento == string.Empty)
                {
                    contador++;
                    return RedirectToAction("GestionarEnvioEncomienda", "EnvioEncomienda", new { error = "Se debe de ingresar Numero de Documento" });
                }
                if (combo == "Seleccionar")
                {
                    contador++;
                    return RedirectToAction("GestionarEnvioEncomienda", "EnvioEncomienda", new { error = "Se debe de seleccionar Tipo de Documento" });
                }

                //FIN VALIDACIONES CAMPOS
                if (contador == 0)
                {

                }
                // Fin Registrar DocumentoPago

                //Capturamos datos de la cabecera PARA REGISTRAR ENVIO
                DocumentoEnvioEncomienda documentoEnvio = new DocumentoEnvioEncomienda();
                Ruta ruta = new Ruta
                {
                    IdRuta = Convert.ToInt16(frm["Ruta"])
                };
                documentoEnvio.Ruta = ruta;
                documentoEnvio.FechaEnvio = Convert.ToDateTime(frm["txtFechaEnvio"]);
                documentoEnvio.ADomicilio = frm["txtDireccion"];
                Cliente clienteEnvio = new Cliente
                {
                    IdCliente = Convert.ToInt16(frm["txtIdClienteEnvio"])
                };
                documentoEnvio.ClienteEnvio = clienteEnvio;
                Cliente clienteEntrega = new Cliente
                {
                    IdCliente = Convert.ToInt16(frm["txtIdClienteEntrega"])
                };
                documentoEnvio.ClienteEntrega = clienteEntrega;
                //VALIDACION DATOS

                if (documentoEnvio.Ruta.Equals(""))
                {
                    contador++;
                    return RedirectToAction("GestionarEnvioEncomienda", "EnvioEncomienda", new { error = "Se debe de seleccionr Ruta" });
                }
                if (documentoEnvio.FechaEnvio.Equals(""))
                {
                    contador++;
                    return RedirectToAction("GestionarEnvioEncomienda", "EnvioEncomienda", new { error = "Se debe de ingresar Fecha DD/MM/AA" });
                }
                if (documentoEnvio.ADomicilio.Equals("") || documentoEnvio.ADomicilio == string.Empty)
                {
                    contador++;
                    return RedirectToAction("GestionarEnvioEncomienda", "EnvioEncomienda", new { error = "Se debe de ingresar Direccion" });
                }
                if (documentoEnvio.ClienteEnvio.Equals("") || clienteEnvio.IdCliente == 0)
                {
                    contador++;
                    return RedirectToAction("GestionarEnvioEncomienda", "EnvioEncomienda", new { error = "Se debe de ingresar Remitente" });
                }
                if (documentoEnvio.ClienteEntrega.Equals("") || clienteEntrega.IdCliente == 0)
                {
                    contador++;
                    return RedirectToAction("GestionarEnvioEncomienda", "EnvioEncomienda", new { error = "Se debe de ingresar Destinatario" });
                }
                //FIN VALIDACION DATOS

                if (contador == 0)
                {
                    Boolean inserto = objEnvioEncomienda.InsertarEnvioEncomienda2(documentoEnvio);
                    Boolean inserto3 = objEnvioEncomienda.InsertarDocumentoPago(documentoPago);

                }
                //fin Registrar Envio

                //Detalle
                List<DetalleDocumentoEnvioEncomienda> detalleEnvio = new List<DetalleDocumentoEnvioEncomienda>();
                DataTable dt = (DataTable)Session["DetEnvio"];
                foreach (DataRow r in dt.Rows)
                {
                    DetalleDocumentoEnvioEncomienda detalle = new DetalleDocumentoEnvioEncomienda
                    {
                        Descripcion = r["Descripcion"].ToString(),
                        Peso = Convert.ToInt16(r["Peso"])
                    };
                    if (dt.Rows.Count > 0 && contador == 0)
                    {
                        Boolean inserto2 = objEnvioEncomienda.InsertarDetalleEnvioEncomienda(detalle);
                    }
                    else
                    {
                        return RedirectToAction("GestionarEnvioEncomienda", "EnvioEncomienda", new { error = "No ingreso ningun Paquete a Enviar" });
                    }
                }
                //Fin detalle
                if (contador > 1)
                {
                    return RedirectToAction("GestionarEnvioEncomienda", "EnvioEncomienda", new { error = "Verificar Ingreso de Datos Completos" });
                }
                Session.Remove("TipoDocumento");
                Session.Remove("DetEnvio");
                Session.Remove("ClienteEntrega");
                Session.Remove("ClienteEnvio");
                Session.Remove("CalcularPrecios");
                //  subtotal = 0;
                // documentoPago.MontoTotal = 0;
                ViewBag.mensaje = "Se registro correctamente el envio de Encomiendas.";
                return View();

            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Error",
                                    new { error = "Ingrese los Datos que se le pide." });
            }
        }
        public ActionResult ListarEnvios()
        {
            Usuario usuario = (Usuario)Session["usuario"];
            Int32 idSucursal = usuario.sucursal.IdSucursal;
            List<DocumentoPago> lista = objEnvioEncomienda.Encomienda_GetByStatus("PENDIENTE", idSucursal);

            if (lista.Count() > 0)
            {
                return PartialView(lista);
            }
            else
            {
                ViewBag.mensaje = "No se encontraron envios pendientes.";
                return View();
            }
        }
        public ActionResult EnviarEncomienda(Int32 idEnvio, FormCollection frm)
        {
            try
            {
                if (idEnvio > 0)
                {
                    DocumentoPago envio = objEnvioEncomienda.Encomienda_GetById(idEnvio);

                    return View(envio);
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
        public ActionResult EnviarEncomienda(FormCollection frm, Int32 idEnvio)
        {
            try
            {

                DocumentoEnvioEncomienda envio = new DocumentoEnvioEncomienda
                {
                    IdDocumentoEnvioEncomienda = idEnvio,
                    ADomicilio = frm["txtDireccion"],
                    FechaEnvio = Convert.ToDateTime(frm["txtFechaEnvio"]),
                    Conductor = frm["txtConductor"]
                };
                Boolean modificar = objEnvioEncomienda.EnvioEncomienda_Update(envio);

                if (modificar)
                {
                    ViewBag.Message = "El envío ha sido actualizado.";
                    return RedirectToAction("ListarEnvios", "EnvioEncomienda");
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error.";
                }


            }
            catch (Exception)
            {

            }
            return RedirectToAction("EnviarEncomienda", "EnvioEncomienda", new { idEnvio });
        }
        [HttpPost]
        public JsonResult Package_Add(Int32 Peso, Int32 idRuta, Double subTotal)
        {
            try
            {
                Tarifa tarifa = objGestionaRuta.listarTarifaIdRuta(idRuta);
                objDetalleEnvio.Peso = Peso;
                objDetalleEnvio.PrecioGramo = tarifa.PrecioGramo;
                Double calcularPrecio = objDetalleEnvio.CalcularPrecio();
                objDetalleEnvio.SubTotal = subTotal + calcularPrecio;
                Double descuento = objDetalleEnvio.CalcularDescuento();
                Double montoTotal = objDetalleEnvio.calcularMontoTotalPagar();
                JavaScriptSerializer sr = new JavaScriptSerializer();
                Object obj = new { result = "Ok", calcularPrecio, Data = sr.Serialize(objDetalleEnvio), montoTotal };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Object obj = new { result = "NoOk", ex.Message };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult TipoDocumento_GetData(String TipoDocumento)
        {
            try
            {
                Usuario usuario = (Usuario)Session["usuario"];
                Int32 idSucursal = usuario.sucursal.IdSucursal;
                DocumentoPago documento = objEnvioEncomienda.ObtenerNumeros(TipoDocumento, idSucursal);
                documento.TipoDocumento = TipoDocumento + " DE ENVÍO";
                JavaScriptSerializer sr = new JavaScriptSerializer();
                Object obj = new { result = "Ok", Data = sr.Serialize(documento) };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Object obj = new { result = "NoOk", ex.Message };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult EnvioEncomienda_Save(DocumentoPago documentoPago)
        {
            try
            {
                // DocumentoPago documentoPago = JsonConvert.DeserializeObject<DocumentoPago>(sendData);
                Boolean inserto = objEnvioEncomienda.InsertarEnvioEncomienda2(documentoPago.DocumentoEnvio);
                Boolean inserto3 = objEnvioEncomienda.InsertarDocumentoPago(documentoPago);


                //Detalle
                List<DetalleDocumentoEnvioEncomienda> detalleEnvio = new List<DetalleDocumentoEnvioEncomienda>();
                DataTable dt = (DataTable)Session["DetEnvio"];
                foreach (DetalleDocumentoEnvioEncomienda r in documentoPago.DocumentoEnvio.detalleEnvio)
                {

                    Boolean inserto2 = objEnvioEncomienda.InsertarDetalleEnvioEncomienda(r);

                }
                Object obj = new { result = "Ok", Message = "Se registro correctamente el envio de Encomiendas." };
                return Json(obj, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Object obj = new { result = "NoOk", ex.Message };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
