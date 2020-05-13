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
    public class ClienteDao:IClienteDAO
    {
          private IGestorDAO gestorDAOSQL;
        public ClienteDao(IGestorDAO gestorDAOSQL)
        {
            this.gestorDAOSQL = gestorDAOSQL;
        }



        #region Metodos
        public int insertarCliente(Cliente cliente)
        {
            SqlCommand cmd = null;
           // Boolean resultado = false;
            SqlConnection cn = null;
           try
           {
                cn = new SqlConnection();
                cn = gestorDAOSQL.abrirConexion();
                cmd = new SqlCommand("sp_InsertarCliente", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TipoDocumento", cliente.TipoDocumento);
                cmd.Parameters.AddWithValue("@numDocumento", cliente.NumeroDocumento);
                cmd.Parameters.AddWithValue("@razonSocial", cliente.RazonSocial);
                cmd.Parameters.AddWithValue("@nombres", cliente.NombreCliente);
                cmd.Parameters.AddWithValue("@apellidoPaterno", cliente.ApellidoPaternoCliente);
                cmd.Parameters.AddWithValue("@apellidoMaterno", cliente.ApellidoMaternoCliente);
                cmd.Parameters.AddWithValue("@telefono", cliente.Telefono);
                cmd.Parameters.AddWithValue("@direccion", cliente.Direccion);
                SqlParameter p = new SqlParameter("@retorno", DbType.Int32);
                p.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(p);
                //if (cmd.ExecuteNonQuery() > 0)
                //{
                //    resultado = true;
                //}
                cmd.ExecuteNonQuery();
                int i = Convert.ToInt32(cmd.Parameters["@retorno"].Value);
                return i;
                }
            catch (Exception e) { throw e; }
          //  return resultado;
        }

        public Boolean modificarCliente(int idCliente, Cliente cliente)
        {
            SqlCommand cmd = null;
            Boolean resultado = false;
            SqlConnection cn = null;
          try
            {
               // cn = Conexio;
                cn = gestorDAOSQL.abrirConexion();
                cmd = new SqlCommand("sp_ActualizarCliente", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TipoDocumento", cliente.TipoDocumento);
                cmd.Parameters.AddWithValue("@numDocumento", cliente.NumeroDocumento);
                cmd.Parameters.AddWithValue("@razonSocial", cliente.RazonSocial);
                cmd.Parameters.AddWithValue("@direcciones", cliente.Direccion);
                cmd.Parameters.AddWithValue("@telefono", cliente.Telefono);
                cmd.Parameters.AddWithValue("@nombre", cliente.NombreCliente);
                cmd.Parameters.AddWithValue("@apellidoPaterno", cliente.ApellidoPaternoCliente);
                cmd.Parameters.AddWithValue("@apellidoMaterno", cliente.ApellidoMaternoCliente);
                cmd.Parameters.AddWithValue("@idCliente", idCliente);
             //  cn.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    resultado = true;
                }
            }
            catch (Exception e) { throw e; }
          //  finally { cmd.Connection.Close(); }
            return resultado;
        }

        public Boolean EliminarCliente(int idCliente)
        {
            SqlCommand cmd = null;
            Boolean resultado = false;
            SqlConnection cn = null;
            try
            {
                cn = new SqlConnection();
                cn = gestorDAOSQL.abrirConexion();
                cmd = new SqlCommand("sp_EliminarCliente", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idCliente", idCliente);
              //  cn.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    resultado = true;
                }
            }
            catch (Exception e) { throw e; }
          //  finally { cmd.Connection.Close(); }
            return resultado;
        }

        public List<Cliente> listarCliente()
        {
            SqlCommand cmd = null;
            List<Cliente> lista = new List<Cliente>(); 
            SqlConnection cn = null;
            try
            {
                 cn = gestorDAOSQL.abrirConexion();
                 cmd = new SqlCommand("sp_listarClientes", cn);
                cmd.CommandType = CommandType.StoredProcedure;
               // cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Cliente cliente = new Cliente();
              
                    cliente.IdCliente = Convert.ToInt32(dr["idCliente"]);
              
                    cliente.TipoDocumento = dr["TipoDocumento"].ToString();
                    cliente.NumeroDocumento = dr["NumDocumento"].ToString();
                    cliente.RazonSocial = dr["RazonSocial"].ToString();
                    cliente.NombreCliente = dr["Nombres"].ToString();
                    cliente.ApellidoPaternoCliente = dr["ApellidoPaterno"].ToString();
                    cliente.ApellidoMaternoCliente = dr["ApellidoMaterno"].ToString();
                    cliente.Direccion = dr["Direccion"].ToString();
                    cliente.Telefono = dr["Telefono"].ToString();
                    lista.Add(cliente);
                }
            }
            catch (Exception e) { throw e; }
           // finally { cmd.Connection.Close(); }
            return lista;
        }

       public Cliente devolverCliente(int idCliente)
        {
            SqlCommand cmd = null;
            Cliente cliente = null;
            SqlConnection cn = null;
            try
            {
                cn = new SqlConnection();
                cn = gestorDAOSQL.abrirConexion();
                cmd = new SqlCommand("sp_devolverCliente", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idCliente", idCliente);
         //       cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    cliente = new Cliente();
                    cliente.IdCliente = Convert.ToInt32(dr["idCliente"]);
                    cliente.TipoDocumento = dr["TipoDocumento"].ToString();
                    cliente.NumeroDocumento = dr["NumDocumento"].ToString();
                    cliente.RazonSocial = dr["RazonSocial"].ToString();
                    cliente.NombreCliente = dr["Nombres"].ToString();
                    cliente.ApellidoPaternoCliente = dr["ApellidoPaterno"].ToString();
                    cliente.ApellidoMaternoCliente = dr["ApellidoMaterno"].ToString();
                    cliente.Telefono = dr["Telefono"].ToString();
                    cliente.Direccion = dr["Direccion"].ToString();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return cliente;
        }
       public Cliente buscarClientePorNumDocumento2(String numDocumento)
       {
           SqlCommand cmd = null;
           Cliente cliente = null;
           SqlConnection cn = null;
           try
           {
               cn = new SqlConnection();
               cn = gestorDAOSQL.abrirConexion();
               cmd = new SqlCommand("sp_buscarCliente", cn);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.AddWithValue("@numDocumento", numDocumento);
               //       cn.Open();
               SqlDataReader dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   cliente = new Cliente();
                   cliente.IdCliente = Convert.ToInt32(dr["idCliente"]);
                   cliente.TipoDocumento = dr["TipoDocumento"].ToString();
                   cliente.NumeroDocumento = dr["NumDocumento"].ToString();
                   cliente.RazonSocial = dr["RazonSocial"].ToString();
                   cliente.NombreCliente = dr["Nombres"].ToString();
                   cliente.ApellidoPaternoCliente = dr["ApellidoPaterno"].ToString();
                   cliente.ApellidoMaternoCliente = dr["ApellidoMaterno"].ToString();
                   cliente.Telefono = dr["Telefono"].ToString();
                   cliente.Direccion = dr["Direccion"].ToString();
               }
           }
           catch (Exception e)
           {
               throw e;
           }
           return cliente;
       }
        public List<Cliente> buscarClientePorNumDocumento(String numDocumento)
       {
           SqlCommand cmd = null;
           Cliente cliente = null;
           List<Cliente> lista = new List<Cliente>(); 
           SqlConnection cn = null;
           try
           {
               cn = new SqlConnection();
               cn = gestorDAOSQL.abrirConexion();
               cmd = new SqlCommand("sp_buscarCliente", cn);
               cmd.CommandType = CommandType.StoredProcedure;
               cmd.Parameters.AddWithValue("@numDocumento", numDocumento);
               //       cn.Open();
               SqlDataReader dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   cliente = new Cliente();
                   cliente.IdCliente = Convert.ToInt32(dr["idCliente"]);
                   cliente.TipoDocumento = dr["TipoDocumento"].ToString();
                   cliente.NumeroDocumento = dr["NumDocumento"].ToString();
                   cliente.RazonSocial = dr["RazonSocial"].ToString();
                   cliente.NombreCliente = dr["Nombres"].ToString();
                   cliente.ApellidoPaternoCliente = dr["ApellidoPaterno"].ToString();
                   cliente.ApellidoMaternoCliente = dr["ApellidoMaterno"].ToString();
                   cliente.Telefono = dr["Telefono"].ToString();
                   cliente.Direccion = dr["Direccion"].ToString();
                   lista.Add(cliente);
               }
           }
           catch (Exception e)
           {
               throw e;
           }
          return lista;
       }

       


        #endregion Metodos
    }
}
