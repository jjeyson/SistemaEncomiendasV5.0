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
   public class GestionarRuta
    {
        private IGestorDAO gestorDAO;
          private IRutaDAO rutaDAO;

          public GestionarRuta() 
        {
            FabricaAbstractaDAO fabricaAbstractaDAO = FabricaAbstractaDAO.getInstancia();
            gestorDAO = fabricaAbstractaDAO.crearGestorDAO();
            rutaDAO = fabricaAbstractaDAO.crearRutaDAO(gestorDAO);
        }
          public List<Ruta> listarRuta()
        {
            try
           {
                List<Ruta> listaRuta = rutaDAO.listarRuta();
                gestorDAO.cerrarConexion();
                return listaRuta;
            }
            catch (Exception e) { throw e; }
        }
          public List<Ruta> listarRutaId(int idRuta)
          {
              try
              {
                  List<Ruta> listaRuta = rutaDAO.listarRutaId(idRuta);
                  gestorDAO.cerrarConexion();
                  return listaRuta;
              }
              catch (Exception e) { throw e; }
          }
          public Tarifa listarTarifaIdRuta(int idRuta)
          {
              try
              {
                  Tarifa tarifa = rutaDAO.listarTarifaIdRuta(idRuta);
                  gestorDAO.cerrarConexion();
                  return tarifa;
              }
              catch (Exception e)
              {
                  throw e;
              }
          }
    }
}
