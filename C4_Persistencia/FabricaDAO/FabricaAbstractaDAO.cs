using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C3_Dominio.Contratos;
namespace C4_Persistencia.FabricaDAO
{
   public abstract class FabricaAbstractaDAO
    {
       public static FabricaAbstractaDAO getInstancia()
       {
           Type tipoFabricaDAO = Type.GetType(C4_Persistencia.FabricaDAO.Parametros.Default.claseFabricaDAO);
           FabricaAbstractaDAO FabricaDAO = (FabricaAbstractaDAO)Activator.CreateInstance(tipoFabricaDAO);
           return FabricaDAO;
       }
       public abstract IGestorDAO crearGestorDAO();
       public abstract IClienteDAO crearClienteDAO(IGestorDAO gestorDAO);
       public abstract IUsuarioDAO crearUsuarioDAO(IGestorDAO gestorDAO);
       public abstract ITipoDocumentoDAO crearTipoDocumentoDAO(IGestorDAO gestorDAO);
       public abstract IDocumentoEnvioEncomienda crearDocumentoEnvioEncomiendaDAO(IGestorDAO gestorDAO);
       public abstract IRutaDAO crearRutaDAO(IGestorDAO gestorDAO);
       public abstract ITipoUsuarioDAO crearTipoUsuario(IGestorDAO gestorDAO);
        public abstract ISucursal crearSucursal(IGestorDAO gestorDAO);
    }
}
