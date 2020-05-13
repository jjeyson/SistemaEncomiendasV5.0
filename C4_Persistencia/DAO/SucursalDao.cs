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
    public class SucursalDao : ISucursal
    {
        private IGestorDAO gestorDAOSQL;
        public SucursalDao(IGestorDAO gestorDAOSQL)
        {
            this.gestorDAOSQL = gestorDAOSQL;
        }
        public List<Sucursal> Sucursal_GetAll()
        {
            SqlCommand cmd = null;
            SqlConnection cn = null;
            List<Sucursal> lista = null;
            try
            {
                cn = new SqlConnection();
                cn = gestorDAOSQL.abrirConexion();
                cmd = new SqlCommand("Sucursal_GetAll_SP", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                lista = new List<Sucursal>();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Sucursal sucursal = new Sucursal
                    {
                        IdSucursal = Convert.ToInt32(dr["idSucursal"]),
                        Ciudad = dr["Ciudad"].ToString(),
                        Direccion = dr["Direccion"].ToString()
                    };
                    lista.Add(sucursal);
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
