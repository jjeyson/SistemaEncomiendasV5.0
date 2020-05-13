using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace C3_Dominio.Contratos
{
    public interface IGestorDAO
    {
        SqlConnection abrirConexion();
        void cerrarConexion();
        void iniciarTransaccion();
        void terminarTransaccion();
        void cancelarTransaccion();
        SqlDataReader ejecutarConsulta(String sentenciaSQL);
        SqlCommand obtenerComandoSQL(String sentenciaSQL);
        SqlCommand obtenerComandoSP(String procedimiento_almacenado);

    }
}
