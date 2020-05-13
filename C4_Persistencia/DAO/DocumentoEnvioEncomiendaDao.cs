using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C3_Dominio.Entidades;
using C3_Dominio.Contratos;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
namespace C4_Persistencia.DAO
{
    public class DocumentoEnvioEncomiendaDao : IDocumentoEnvioEncomienda
    {
        private IGestorDAO gestorDAOSQL;
        public DocumentoEnvioEncomiendaDao(IGestorDAO gestorDAOSQL)
        {
            this.gestorDAOSQL = gestorDAOSQL;
        }
        public int InsertarEnvioEncomienda(String cadxml)
        {
            SqlCommand cmd = null;
            SqlConnection cn = null;
            cn = new SqlConnection();
            cn = gestorDAOSQL.abrirConexion();
            cmd = new SqlCommand("sp_InsertarEncomienda", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@prmcadXML", cadxml);
            SqlParameter p = new SqlParameter("@retorno", DbType.Int32);
            p.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(p);
            int i = Convert.ToInt32(cmd.Parameters["@retorno"].Value);
            return i;

        }
        public Boolean InsertarDetalleEnvioEncomienda(DetalleDocumentoEnvioEncomienda detalleEnvio)
        {

            SqlCommand cmd = null;
            Boolean resultado = false;
            SqlConnection cn = null;
            DocumentoEnvioEncomienda documentoEnvio = new DocumentoEnvioEncomienda();
            try
            {
                cn = new SqlConnection();
                cn = gestorDAOSQL.abrirConexion();
                cmd = new SqlCommand("sp_InsertaDetalleEnvioEncomienda", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Descripcion", detalleEnvio.Descripcion);
                cmd.Parameters.AddWithValue("@Peso", detalleEnvio.Peso);

                if (cmd.ExecuteNonQuery() > 0)
                {
                    resultado = true;
                }
            }
            catch (Exception e) { throw e; }
            return resultado;


        }
        public Boolean InsertarEnvioEncomienda2(DocumentoEnvioEncomienda documentoEnvio)
        {
            SqlCommand cmd = null;
            Boolean resultado = false;
            SqlConnection cn = null;
            try
            {
                cn = new SqlConnection();
                cn = gestorDAOSQL.abrirConexion();
                cmd = new SqlCommand("sp_InsertaEnvioEncomienda", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idRuta", documentoEnvio.Ruta.IdRuta);
                cmd.Parameters.AddWithValue("@aDomicilio", documentoEnvio.ADomicilio);
                cmd.Parameters.AddWithValue("@fechaEnvio", documentoEnvio.FechaEnvio);
                cmd.Parameters.AddWithValue("@idClienteEnvio", documentoEnvio.ClienteEnvio.IdCliente);
                cmd.Parameters.AddWithValue("@idClienteEntrega", documentoEnvio.ClienteEntrega.IdCliente);

                if (cmd.ExecuteNonQuery() > 0)
                {
                    resultado = true;
                }
            }
            catch (Exception e) { throw e; }
            return resultado;
        }


        public Boolean InsertarDocumentoPago(DocumentoPago documentoPago)
        {
            SqlCommand cmd = null;
            Boolean resultado = false;
            SqlConnection cn = null;
            try
            {
                cn = new SqlConnection();
                cn = gestorDAOSQL.abrirConexion();
                cmd = new SqlCommand("sp_InsertarDocumentoPago", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@numSerie", documentoPago.NumSerie);
                cmd.Parameters.AddWithValue("@NumDocumento", documentoPago.NumDocumento);
                cmd.Parameters.AddWithValue("@tipoDocumentoPago", documentoPago.TipoDocumento);
                cmd.Parameters.AddWithValue("@monto", documentoPago.Monto);

                if (cmd.ExecuteNonQuery() > 0)
                {
                    resultado = true;
                }
            }
            catch (Exception e) { throw e; }
            return resultado;
        }


        public List<DetalleDocumentoEnvioEncomienda> buscarClienteEntregaPendiente(String numDocumento, int idSucursal)
        {
            SqlCommand cmd = null;
            DetalleDocumentoEnvioEncomienda detalle = null;
            List<DetalleDocumentoEnvioEncomienda> lista = new List<DetalleDocumentoEnvioEncomienda>();
            SqlConnection cn = null;
            try
            {
                cn = new SqlConnection();
                cn = gestorDAOSQL.abrirConexion();
                cmd = new SqlCommand("sp_buscarEncomienda", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@nDocumento", numDocumento);
                cmd.Parameters.AddWithValue("@idSucursal", idSucursal);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    detalle = new DetalleDocumentoEnvioEncomienda();
                    detalle.Descripcion = dr["Descripción"].ToString();
                    detalle.Peso = Convert.ToInt32(dr["Peso"]);
                    DocumentoEnvioEncomienda envio = new DocumentoEnvioEncomienda();
                    envio.FechaEnvio = Convert.ToDateTime(dr["FechaEnvio"]);
                    Cliente cliente = new Cliente();
                    cliente.NombreCliente = dr["Remitente"].ToString();
                    envio.ClienteEnvio = cliente;
                    detalle.DocumentoEnvio = envio;

                    lista.Add(detalle);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return lista;
        }


        public Boolean insertarRecepcionEncomienda(int idCliente)
        {
            SqlCommand cmd = null;
            Boolean resultado = false;
            SqlConnection cn = null;
            try
            {
                cn = gestorDAOSQL.abrirConexion();
                cmd = new SqlCommand("sp_insertarRecepcionEncomienda", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idCliente", idCliente);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    resultado = true;
                }
            }
            catch (Exception e) { throw e; }
            return resultado;
        }

        public List<DocumentoPago> ListarReporte(String Desde, String Hasta, Int32 idSucursal)

        {
            SqlCommand cmd = null;
            DocumentoPago documento = null;
            List<DocumentoPago> lista = new List<DocumentoPago>();
            SqlConnection cn = null;
            try
            {
                cn = new SqlConnection();
                cn = gestorDAOSQL.abrirConexion();
                cmd = new SqlCommand("sp_reporteIngresosFechas", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fechaInicio", Convert.ToDateTime(Desde));
                cmd.Parameters.AddWithValue("@fechaFin", Convert.ToDateTime(Hasta));
                 cmd.Parameters.AddWithValue("@Idsucursal", idSucursal);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    documento = new DocumentoPago();
                    documento.TipoDocumento = dr["TipoDocumentoPago"].ToString();
                    documento.NumSerie = dr["NumSerie"].ToString();
                    documento.NumDocumento = dr["NumDocumento"].ToString();
                    documento.Monto = Convert.ToDouble(dr["Monto"]);
                    DocumentoEnvioEncomienda envio = new DocumentoEnvioEncomienda();
                    envio.FechaEnvio = Convert.ToDateTime(dr["FechaEnvio"]);
                    Cliente cliente = new Cliente
                    {
                        NombreCliente = dr["Remitente"].ToString()
                    };
                    envio.ClienteEnvio = cliente;
                    Cliente cliente2 = new Cliente
                    {
                        NombreCliente = dr["Destinatario"].ToString()
                    };
                    envio.ClienteEntrega = cliente2;
                    Ruta ruta = new Ruta
                    {
                        Descripcion = dr["Descripcion"].ToString()
                    };
                    envio.Ruta = ruta;
                    documento.DocumentoEnvio = envio;
                    lista.Add(documento);
                }
            }
            catch (Exception e) { throw e; }
            return lista;
        }


        public DocumentoPago MontoTotalReporte(String Desde, String Hasta, Int32 idSucursal)
        {
            SqlCommand cmd = null;
            DocumentoPago documento = null;
            SqlConnection cn = null;
            try
            {
                cn = new SqlConnection();
                cn = gestorDAOSQL.abrirConexion();
                cmd = new SqlCommand("sp_reporteMontoTotalFechas", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@fechaInicio", Convert.ToDateTime(Desde));
                cmd.Parameters.AddWithValue("@fechaFin", Convert.ToDateTime(Hasta));
                cmd.Parameters.AddWithValue("@Idsucursal", idSucursal);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    documento = new DocumentoPago();
                    documento.Monto = Convert.ToDouble(dr["Monto"]);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return documento;
        }

        public DocumentoPago ObtenerNumeros(String tipoDocumento, int idSucursal)
        {
            SqlCommand cmd = null;
            DocumentoPago documento = null;
            SqlConnection cn = null;
            try
            {
                cn = new SqlConnection();
                cn = gestorDAOSQL.abrirConexion();
                cmd = new SqlCommand("sp_obtenerNumeros", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TipoDoc", tipoDocumento);
                cmd.Parameters.AddWithValue("@idSucursal", idSucursal);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    documento = new DocumentoPago();
                    documento.NumSerie = dr["NumSerie"].ToString();
                    documento.NumDocumento = dr["NumDocumento"].ToString();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return documento;
        }


        public List<DocumentoPago> Encomienda_GetByStatus(String estado, Int32 idSucursal)

        {
            SqlCommand cmd = null;
            DocumentoPago documento = null;
            List<DocumentoPago> lista = new List<DocumentoPago>();
            SqlConnection cn = null;
            try
            {
                cn = new SqlConnection();
                cn = gestorDAOSQL.abrirConexion();
                cmd = new SqlCommand("Encomienda_GetByStatus_Sp", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@status", estado);
                cmd.Parameters.AddWithValue("@Idsucursal", idSucursal);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    documento = new DocumentoPago();
                    documento.TipoDocumento = dr["TipoDocumentoPago"].ToString();
                    documento.NumSerie = dr["NumSerie"].ToString();
                    documento.NumDocumento = dr["NumDocumento"].ToString();
                    documento.Monto = Convert.ToDouble(dr["Monto"]);
                    DocumentoEnvioEncomienda envio = new DocumentoEnvioEncomienda
                    {
                        FechaEnvio = Convert.ToDateTime(dr["FechaEnvio"]),
                        Estado = dr["Estado"].ToString(),
                        IdDocumentoEnvioEncomienda = Convert.ToInt32(dr["idDocumentoEnvioEncomienda"])
                    };
                    Cliente cliente = new Cliente
                    {
                        NombreCliente = dr["Remitente"].ToString()
                    };
                    envio.ClienteEnvio = cliente;
                    Cliente cliente2 = new Cliente
                    {
                        NombreCliente = dr["Destinatario"].ToString()
                    };
                    envio.ClienteEntrega = cliente2;
                    Ruta ruta = new Ruta
                    {
                        Descripcion = dr["Descripcion"].ToString()
                    };
                    envio.Ruta = ruta;
                    documento.DocumentoEnvio = envio;
                    lista.Add(documento);
                }
            }
            catch (Exception e) { throw e; }
            return lista;
        }

        public DocumentoPago Encomienda_GetById(Int32 idEnvio)

        {
            SqlCommand cmd = null;
            DocumentoPago documento = null;
            SqlConnection cn = null;
            try
            {
                cn = new SqlConnection();
                cn = gestorDAOSQL.abrirConexion();
                cmd = new SqlCommand("Encomienda_GetById_Sp", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idEnvio", idEnvio);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    documento = new DocumentoPago();
                    documento.TipoDocumento = dr["TipoDocumentoPago"].ToString() + " " + dr["NumSerie"].ToString() + "-" + dr["NumDocumento"].ToString();
                    documento.NumSerie = dr["NumSerie"].ToString();
                    documento.NumDocumento = dr["NumDocumento"].ToString();
                    documento.Monto = Convert.ToDouble(dr["Monto"]);
                    DocumentoEnvioEncomienda envio = new DocumentoEnvioEncomienda
                    {
                        FechaEnvio = DateTime.ParseExact(dr["FechaEnvio"].ToString(), "dd/MM/yyyy", null),
                        Estado = dr["Estado"].ToString(),
                        ADomicilio = dr["A_Domicilio"].ToString(),
                        CantidadPaquetes2 = Convert.ToInt32(dr["CantidadPaquetes"])
                    };

                    documento.DocumentoEnvio = envio;
                }
            }
            catch (Exception e) { throw e; }
            return documento;
        }

        public Boolean EnvioEncomienda_Update(DocumentoEnvioEncomienda documentoEnvio)
        {
            SqlCommand cmd = null;
            Boolean resultado = false;
            SqlConnection cn = null;
            try
            {
                cn = new SqlConnection();
                cn = gestorDAOSQL.abrirConexion();
                cmd = new SqlCommand("[DocumentoEnvioEncomienda_UpdateEnvio_Sp]", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idDocumentoEnvioEncomienda", documentoEnvio.IdDocumentoEnvioEncomienda);
                cmd.Parameters.AddWithValue("@A_Domicilio", documentoEnvio.ADomicilio);
                cmd.Parameters.AddWithValue("@fechaEnvio", documentoEnvio.FechaEnvio);
                cmd.Parameters.AddWithValue("@ConductorNombre", documentoEnvio.Conductor);

                if (cmd.ExecuteNonQuery() > 0)
                {
                    resultado = true;
                }
            }
            catch (Exception e) { throw e; }
            return resultado;
        }

    }
}
