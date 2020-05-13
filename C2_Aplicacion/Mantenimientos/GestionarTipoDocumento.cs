using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C3_Dominio.Entidades;
using C3_Dominio.Contratos;
using C4_Persistencia.FabricaDAO;
using C4_Persistencia.DAO;
namespace C2_Aplicacion.Mantenimientos
{
    public class GestionarTipoDocumento
    {
          private IGestorDAO gestorDAO;
          private ITipoDocumentoDAO tipoDocumentoDAO;

        public GestionarTipoDocumento() 
        {
            FabricaAbstractaDAO fabricaAbstractaDAO = FabricaAbstractaDAO.getInstancia();
            gestorDAO = fabricaAbstractaDAO.crearGestorDAO();
            tipoDocumentoDAO = fabricaAbstractaDAO.crearTipoDocumentoDAO(gestorDAO);
        }
        public List<TipoDocumento> listarTipoDocumento()
        {
            try
           {
                List<TipoDocumento> listaTipoDocumento = tipoDocumentoDAO.listarTipoDocumento();
                gestorDAO.cerrarConexion();
                return listaTipoDocumento;
            }
            catch (Exception e) { throw e; }
        }
    }
}
