using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C3_Dominio.Entidades;
namespace C3_Dominio.Contratos
{
    public interface IUsuarioDAO
    {
        Usuario inicioSesion(String usuarios, String clave);
        List<Usuario> listarUsuarios(Int32 idSucursal);
        Int32 Usuario_Save(Usuario usuario);
        Usuario Usuario_GetByID(Int32 idUsuario);
        Boolean Usuario_Delete(Int32 idUsuario);
    }
}
