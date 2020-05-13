using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C3_Dominio.Entidades;
using C3_Dominio.Contratos;
using System.Data;
using System.Data.SqlClient;
namespace C4_Persistencia.DAO
{
 public   class RutaDao:IRutaDAO
    {
         private IGestorDAO gestorDAOSQL;
         public RutaDao(IGestorDAO gestorDAOSQL)
        {
            this.gestorDAOSQL = gestorDAOSQL;
        }
       
         public List<Ruta> listarRuta()
         {
             SqlCommand cmd = null;
             List<Ruta> lista = new List<Ruta>();
             SqlConnection cn = null;
             try
             {
                 cn = gestorDAOSQL.abrirConexion();
                 //  cn = gestorDAOSQL.abrirConexion();
                 cmd = new SqlCommand("sp_listarRutas", cn);
                 cmd.CommandType = CommandType.StoredProcedure;
                 //  cn.Open();
                 SqlDataReader dr = cmd.ExecuteReader();
                 while (dr.Read())
                 {

                     Ruta ruta = new Ruta();
                     ruta.IdRuta = Convert.ToInt32(dr["idRuta"]);
                     ruta.Descripcion = dr["Descripcion"].ToString();

                     lista.Add(ruta);
                 }
             }
             catch (Exception e) { throw e; }

             //  finally { cmd.Connection.Close(); }
             return lista;
         }
         public List<Ruta> listarRutaId(int idRuta)
         {
             SqlCommand cmd = null;
             List<Ruta> lista = new List<Ruta>();
             SqlConnection cn = null;

             Ruta ruta = null;
             try
             {
                 cn = gestorDAOSQL.abrirConexion();
                 cmd = new SqlCommand("sp_SeleccionarRuta", cn);
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.Parameters.AddWithValue("@idSucursalOrigen", idRuta);
                 SqlDataReader dr = cmd.ExecuteReader();
                 while (dr.Read())
                 {

                     ruta = new Ruta();
                     ruta.IdRuta = Convert.ToInt32(dr["idRuta"]);
                    // ruta.SucursalOrigen.IdSucursal = Convert.ToInt32(dr["idSucursalOrigen"]);
                     ruta.Descripcion = dr["Descripcion"].ToString();

                     lista.Add(ruta);
                 }
             }
             catch (Exception e)
             {
                 throw e;
             }
             return lista;
         }
         public Tarifa listarTarifaIdRuta(int idRuta)
         {
             SqlCommand cmd = null;
             Tarifa tarifa = null;
             SqlConnection cn = null;
             try
             {
                 cn = new SqlConnection();
                 cn = gestorDAOSQL.abrirConexion();
                 cmd = new SqlCommand("sp_obtenerPrecioGramo", cn);
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.Parameters.AddWithValue("@idRuta", idRuta);
                 //       cn.Open();
                 SqlDataReader dr = cmd.ExecuteReader();
                 if (dr.Read())
                 {
                     tarifa = new Tarifa();
                     tarifa.IdTarifa = Convert.ToInt32(dr["idTarifa"]);
                     tarifa.PrecioGramo = Convert.ToDouble(dr["PrecioGramo"]);
        
                 }
             }
             catch (Exception e)
             {
                 throw e;
             }
             return tarifa;
         }
 
 }
}
