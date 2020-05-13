using C3_Dominio.Contratos;
using C3_Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C4_Persistencia.DAO
{
    public class TipoUsuarioDao : ITipoUsuarioDAO
    {
        private IGestorDAO gestorDAOSQL;
        public TipoUsuarioDao(IGestorDAO gestorDAOSQL)
        {
            this.gestorDAOSQL = gestorDAOSQL;
        }

        public List<TipoUsuario> listaTipoUsuario()
        {
            SqlCommand cmd = null;
            SqlConnection cn = null;
 
            try
            {
                List<TipoUsuario> lista = new List<TipoUsuario>(); 
                cn = new SqlConnection();
                cn = gestorDAOSQL.abrirConexion();
                cmd = new SqlCommand("TioUSuario_GetAll_SP", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TipoUsuario tipoUsuario = new TipoUsuario
                    {
                        id = Convert.ToInt32(dr["idUsuario"]),
                        nombre = Convert.ToString(dr["nombre"]),
                        estado = Convert.ToBoolean(dr["estado"])
                    };
                    lista.Add(tipoUsuario);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<TipoUsuario> buscarPorId(int id)
        {
            SqlCommand cmd = null;
            SqlConnection cn = null;
            List<TipoUsuario> lista = null;
            try
            {
                cn = new SqlConnection();
                cn = gestorDAOSQL.abrirConexion();
                cmd = new SqlCommand("select * from TipoUsuario as tu Where tu.id = '"+id+"", cn);
                cmd.CommandType = CommandType.Text;
                lista = new List<TipoUsuario>();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TipoUsuario tipoUsuario = new TipoUsuario();
                    tipoUsuario.id = Convert.ToInt32(dr["1"]);
                    tipoUsuario.nombre = Convert.ToString(dr["2"]);
                    tipoUsuario.estado = Convert.ToBoolean(dr["3"]);
                    lista.Add(tipoUsuario);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
