using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C3_Dominio.Contratos;
using C3_Dominio.Entidades;
using System.Data.SqlClient;
using System.Data;
namespace C4_Persistencia.DAO
{
   public class TipoDocumentoDao : ITipoDocumentoDAO
    {
            private IGestorDAO gestorDAOSQL;
        public TipoDocumentoDao(IGestorDAO gestorDAOSQL)
        {
            this.gestorDAOSQL = gestorDAOSQL;
        }
        #region Metodos
        public List<TipoDocumento> listarTipoDocumento()
        {
            SqlCommand cmd = null;
            List<TipoDocumento> lista = new List<TipoDocumento>();
            SqlConnection cn = null;
            try
            {
                cn = gestorDAOSQL.abrirConexion();
                //  cn = gestorDAOSQL.abrirConexion();
                cmd = new SqlCommand("sp_listarTipoDocumento", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                //  cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    TipoDocumento tipoDocumento = new TipoDocumento();
                    tipoDocumento.IdTipoDocumento = Convert.ToInt32(dr["IdTipoDocumento"]);
                    tipoDocumento.Descripcion = dr["Descripcion"].ToString();

                    lista.Add(tipoDocumento);
                }
            }
            catch (Exception e) { throw e; }

            finally { cmd.Connection.Close(); }
            return lista;
        }



        #endregion Metodos
    }
}
