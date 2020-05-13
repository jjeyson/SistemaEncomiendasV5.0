using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C3_Dominio.Contratos;
using C4_Persistencia.FabricaDAO;
namespace C4_Persistencia.DAO
{
  public  class FabricaDAOSQL:FabricaAbstractaDAO
    {
        override
         public IGestorDAO crearGestorDAO()
        {
            return new GestorDAOSql();
        }

        override
        public IClienteDAO crearClienteDAO(IGestorDAO gestorDAO)
        {
            return new ClienteDao(gestorDAO);
        }
        override
      public IUsuarioDAO crearUsuarioDAO(IGestorDAO gestorDAO)
        {
            return new UsuarioDao(gestorDAO);
        }
        override
       public ITipoDocumentoDAO crearTipoDocumentoDAO(IGestorDAO gestorDAO)
        {
            return new TipoDocumentoDao(gestorDAO);
        }
        override
        public IDocumentoEnvioEncomienda crearDocumentoEnvioEncomiendaDAO(IGestorDAO gestorDAO)
        {
            return new DocumentoEnvioEncomiendaDao(gestorDAO);
        }
        override
        public IRutaDAO crearRutaDAO(IGestorDAO gestorDAO)
        {
            return new RutaDao(gestorDAO);
        }
        override
        public  ITipoUsuarioDAO crearTipoUsuario(IGestorDAO gestorDAO)
        {
            return new TipoUsuarioDao(gestorDAO);
        }

        public override ISucursal crearSucursal(IGestorDAO gestorDAO)
        {
            return new SucursalDao(gestorDAO);
        }
    }
}
