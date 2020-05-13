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
    public class UsuarioDao : IUsuarioDAO
    {
        private IGestorDAO gestorDAOSQL;
        public UsuarioDao(IGestorDAO gestorDAOSQL)
        {
            this.gestorDAOSQL = gestorDAOSQL;
        }
        #region Metodos
        public Usuario inicioSesion(String usuarios, String clave)
        {
            SqlCommand cmd = null;
            Usuario usuario = null;
            SqlConnection cn = null;
            try
            {
                cn = new SqlConnection();
                cn = gestorDAOSQL.abrirConexion();
                cmd = new SqlCommand("sp_IniciarSesion", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@usuario", usuarios);
                cmd.Parameters.AddWithValue("@clave", clave);
                //  cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    usuario = new Usuario
                    {
                        IdUsuario = Convert.ToInt32(dr["idUsuario"]),
                        Usuarios = dr["Usuario"].ToString(),
                        Clave = dr["Clave"].ToString(),
                        NombreUsuario = dr["NombreUsuario"].ToString(),
                        ApellidosUsuario = dr["ApellidosUsuario"].ToString(),
                        DNI = dr["DNI"].ToString(),
                        Direccion = dr["Direccion"].ToString(),
                        Telefono = dr["Telefono"].ToString(),
                        Cargo = dr["Cargo"].ToString()
                    };
                    Sucursal sucursal = new Sucursal
                    {
                        IdSucursal = Convert.ToInt32(dr["idSucursal"]),
                        Ciudad = dr["Ciudad"].ToString(),
                        Direccion = dr["SucursalDireccion"].ToString(),
                        Telefono = dr["Telefono"].ToString()
                    };
                    usuario.sucursal = sucursal;
                    TipoUsuario tipoUsuario = new TipoUsuario
                    {
                        id = Convert.ToInt32(dr["tipoUsuario"]),
                        nombre = Convert.ToString(dr["nombre"])
                    };
                    usuario.tipoUsuario = tipoUsuario;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            // finally { cmd.Connection.Close(); }
            return usuario;

        }
        public List<Usuario> listarUsuarios(Int32 idSucursal)
        {
            SqlCommand comando = null;
            SqlDataReader dr = null;
            SqlConnection conexion = null;
            Usuario usuario = null;

            try
            {
                List<Usuario> lista = new List<Usuario>();
                conexion = gestorDAOSQL.abrirConexion();
                comando = new SqlCommand("sp_listarUsuarios", conexion)
                {
                    CommandType = CommandType.StoredProcedure
                };
                comando.Parameters.AddWithValue("@idsucursal", idSucursal);
                dr = comando.ExecuteReader();
                while (dr.Read())
                {
                    usuario = new Usuario
                    {
                        IdUsuario = Convert.ToInt32(dr["idUsuario"]),
                        Usuarios = dr["Usuario"].ToString(),
                        Clave = dr["Clave"].ToString(),
                        NombreUsuario = dr["NombreUsuario"].ToString(),
                        ApellidosUsuario = dr["ApellidosUsuario"].ToString(),
                        DNI = dr["DNI"].ToString(),
                        Direccion = dr["Direccion"].ToString(),
                        Telefono = dr["Telefono"].ToString(),
                        Cargo = dr["Cargo"].ToString()
                    };
                    Sucursal sucursal = new Sucursal
                    {
                        IdSucursal = Convert.ToInt32(dr["idSucursal"]),
                        Ciudad = Convert.ToString(dr["ciudad"])
                    };
                    usuario.sucursal = sucursal;
                    TipoUsuario tipoUsuario = new TipoUsuario
                    {
                        id = Convert.ToInt32(dr["tipoUsuario"]),
                        nombre = Convert.ToString(dr["nombre"])
                    };
                    usuario.tipoUsuario = tipoUsuario;
                    lista.Add(usuario);

                }
                return lista;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public Int32 Usuario_Save(Usuario usuario)
        {
            Int32 result = 0;
            SqlCommand cmd = null;
            SqlConnection cn = null;
            try
            {
                cn = new SqlConnection();
                cn = gestorDAOSQL.abrirConexion();
                cmd = new SqlCommand("Usuario_Save_Sp", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@idUsuario", usuario.IdUsuario);
                cmd.Parameters.AddWithValue("@idSucursal", usuario.sucursal.IdSucursal);
                cmd.Parameters.AddWithValue("@Usuario", usuario.Usuarios);
                cmd.Parameters.AddWithValue("@Clave", usuario.Clave);
                cmd.Parameters.AddWithValue("@NombreUsuario", usuario.NombreUsuario);
                cmd.Parameters.AddWithValue("@ApellidosUsuario", usuario.ApellidosUsuario);
                cmd.Parameters.AddWithValue("@DNI", usuario.DNI);
                cmd.Parameters.AddWithValue("@Direccion", usuario.Direccion);
                cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                cmd.Parameters.AddWithValue("@tipoUsuario", usuario.tipoUsuario.id);
                cmd.Parameters.AddWithValue("@Activo", usuario.Activo);
                result = cmd.ExecuteNonQuery();

            }
            catch (Exception e) { result = 0; throw e; }
            return result;
        }
        public Usuario Usuario_GetByID(Int32 idUsuario)
        {
            SqlCommand cmd = null;
            Usuario usuario = null;
            SqlConnection cn = null;
            try
            {
                cn = new SqlConnection();
                cn = gestorDAOSQL.abrirConexion();
                cmd = new SqlCommand("Usario_GetById_SP", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@idUSuario", idUsuario);
                //       cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    usuario = new Usuario
                    {
                        IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                        Usuarios = dr["Usuario"].ToString(),
                        Clave = dr["Clave"].ToString(),
                        NombreUsuario = dr["NombreUsuario"].ToString(),
                        ApellidosUsuario = dr["ApellidosUsuario"].ToString(),
                        DNI = dr["DNI"].ToString(),
                        Cargo = dr["Cargo"].ToString(),
                        Telefono = dr["Telefono"].ToString(),
                        Direccion = dr["Direccion"].ToString(),
                        Activo = Convert.ToBoolean(dr["Activo"]),
                        sucursal = new Sucursal
                        {
                            IdSucursal = Convert.ToInt32(dr["IdSucursal"]),
                        },
                        tipoUsuario = new TipoUsuario
                        {
                            id = Convert.ToInt32(dr["tipoUsuario"]),
                        }
                    };
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return usuario;
        }

        public Boolean Usuario_Delete(Int32 idUsuario)
        {
            SqlCommand cmd = null;
            Boolean resultado = false;
            SqlConnection cn = null;
            try
            {
                cn = new SqlConnection();
                cn = gestorDAOSQL.abrirConexion();
                cmd = new SqlCommand("Usuario_Delete_Sp", cn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
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
        #endregion Metodos
    }
}
