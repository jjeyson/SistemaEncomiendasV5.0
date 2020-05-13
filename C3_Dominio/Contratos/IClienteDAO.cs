using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C3_Dominio.Entidades;
namespace C3_Dominio.Contratos
{
    public interface IClienteDAO
    {
      //  Boolean insertarCliente(Cliente cliente);
        int insertarCliente(Cliente cliente);
      //  Boolean modificarCliente(int idCliente, Cliente cliente);
        Boolean modificarCliente(int idCliente, Cliente cliente);
        Boolean EliminarCliente(int idCliente);
        List<Cliente> listarCliente();
        Cliente devolverCliente(int idCliente);
        List<Cliente> buscarClientePorNumDocumento(String numDocumento);
        Cliente buscarClientePorNumDocumento2(String numDocumento);
       // List<Cliente> buscarClienteEntrega(String numDocumento);
    }
}
