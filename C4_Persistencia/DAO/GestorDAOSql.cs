using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C3_Dominio.Contratos;
using System.Data.SqlClient;
using System.Data;
namespace C4_Persistencia.DAO
{
   public class GestorDAOSql : IGestorDAO
    {
        private SqlConnection conexion;
        private SqlTransaction transaccion;
        #region Singleton
        private static readonly GestorDAOSql _instancia = new GestorDAOSql();
        public static GestorDAOSql Instancia
        {
            get { return GestorDAOSql._instancia; }
        }
        #endregion Singleton

    
        public SqlConnection abrirConexion()
        {
            try
            {
                conexion = new SqlConnection();
                conexion.ConnectionString = "Data Source=sql7002.site4now.net;Initial Catalog=DB_A4909B_SistemaEncomiendas;Persist Security Info=True;User ID=DB_A4909B_SistemaEncomiendas_admin;Password=1234calidad";
                conexion.Open();
            }
            catch (Exception e)
            {
                throw e;
            }
            return conexion;

        }

        public void cerrarConexion()
        {
            try
            {
                conexion.Close();
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public void iniciarTransaccion()
        {
            try
            {
                abrirConexion();
                transaccion = conexion.BeginTransaction();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void terminarTransaccion()
        {
            try
            {
                transaccion.Commit();
                conexion.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void cancelarTransaccion()
        {
            try
            {
                transaccion.Rollback();
                conexion.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public SqlDataReader ejecutarConsulta(String sentenciaSQL)
        {
            try
            {
                SqlCommand comando = conexion.CreateCommand();
                if (transaccion != null)
                    comando.Transaction = transaccion;
                comando.CommandText = sentenciaSQL;
                comando.CommandType = CommandType.Text;
                SqlDataReader resultado = comando.ExecuteReader();
                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public SqlCommand obtenerComandoSQL(String sentenciaSQL)
        {
            try
            {
                SqlCommand comando = conexion.CreateCommand();
                if (transaccion != null)
                    comando.Transaction = transaccion;
                comando.CommandText = sentenciaSQL;
                comando.CommandType = CommandType.Text;
                return comando;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public SqlCommand obtenerComandoSP(String procedimiento_almacenado)
        {
            try
            {
                SqlCommand comando = conexion.CreateCommand();
                if (transaccion != null)
                    comando.Transaction = transaccion;
                comando.CommandText = procedimiento_almacenado;
                comando.CommandType = CommandType.StoredProcedure;
                return comando;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
