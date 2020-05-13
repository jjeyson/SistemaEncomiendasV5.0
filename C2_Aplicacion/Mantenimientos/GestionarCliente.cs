using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C3_Dominio.Entidades;
using C3_Dominio.Contratos;
using C4_Persistencia.DAO;
using C4_Persistencia.FabricaDAO;
namespace C2_Aplicacion.Mantenimientos
{
    public class GestionarCliente
    {
        #region metodos
         private IGestorDAO gestorDAO;  
        private IClienteDAO clienteDAO;
                public GestionarCliente() 
        {
            FabricaAbstractaDAO fabricaAbstractaDAO = FabricaAbstractaDAO.getInstancia();
            gestorDAO = fabricaAbstractaDAO.crearGestorDAO();
            clienteDAO = fabricaAbstractaDAO.crearClienteDAO(gestorDAO);
        }
        public int insertarCliente(Cliente cliente)
        { 
           
            
           try
           {
                int insertar = clienteDAO.insertarCliente(cliente);
                gestorDAO.cerrarConexion();
                return insertar;
           }
           catch (Exception e)
            {
                throw e;
           }
        }
      
        public Boolean modificarCliente(int idCliente, Cliente cliente)
        {
            

            try
            {
                Boolean modificar = clienteDAO.modificarCliente(idCliente,cliente);
                gestorDAO.cerrarConexion();
                return modificar;
           }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Boolean EliminarCliente(int idCliente)
        {
            Boolean elimina;

            try
            {
                elimina = clienteDAO.EliminarCliente(idCliente);
                gestorDAO.cerrarConexion();
                return elimina;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Cliente> listarCliente()
        {
            try
            {
                List<Cliente> listaCliente;
                listaCliente = clienteDAO.listarCliente();
                gestorDAO.cerrarConexion();
                return listaCliente;
            }
            catch (Exception e) { throw e; }
        }
        public Cliente devolverCliente(int idCliente)
        {
            try
            {
                Cliente cliente = clienteDAO.devolverCliente(idCliente);
                gestorDAO.cerrarConexion();
                return cliente;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Cliente buscarClientePorNumDocumento2(String numDocumento)
        {
            try
            {
                Cliente cliente = clienteDAO.buscarClientePorNumDocumento2(numDocumento);
                gestorDAO.cerrarConexion();
                return cliente;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Cliente> buscarClientePorNumDocumento(String numDocumento)
        {
            try
            {
                List<Cliente> listaCliente;
                 listaCliente = clienteDAO.buscarClientePorNumDocumento(numDocumento);
                gestorDAO.cerrarConexion();
                return listaCliente;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion metodos

    }
}
