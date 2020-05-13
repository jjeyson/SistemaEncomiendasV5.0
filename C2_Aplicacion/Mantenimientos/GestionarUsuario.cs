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
    public class GestionarUsuario
    {

        private IGestorDAO gestorDAO;
        private IUsuarioDAO usuarioDAO;
        private ITipoUsuarioDAO tipoUsuarioDao;
        private ISucursal sucursalDao;
        public GestionarUsuario()
        {
            FabricaAbstractaDAO fabricaAbstractaDAO = FabricaAbstractaDAO.getInstancia();
            gestorDAO = fabricaAbstractaDAO.crearGestorDAO();
            usuarioDAO = fabricaAbstractaDAO.crearUsuarioDAO(gestorDAO);
            tipoUsuarioDao = fabricaAbstractaDAO.crearTipoUsuario(gestorDAO);
            sucursalDao = fabricaAbstractaDAO.crearSucursal(gestorDAO);
        }
        #region Metodos
        public Usuario inicioSesion(String usuario, String clave)
        {
            try
            {
                Usuario objUsuario = usuarioDAO.inicioSesion(usuario, clave);
                gestorDAO.cerrarConexion();
                return objUsuario;
            }
            catch (ApplicationException z)
            {
                throw z;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Usuario> listarUsuario(Int32 idSucursal)
        {
            try
            {
                List<Usuario> lista = usuarioDAO.listarUsuarios(idSucursal);
                gestorDAO.cerrarConexion();
                return lista;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Int32 Usuario_Save(Usuario usuario)
        {


            try
            {
                Int32 insertar = usuarioDAO.Usuario_Save(usuario);
                gestorDAO.cerrarConexion();
                return insertar;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Usuario Usuario_GetByID(Int32 idUSuario)
        {
            try
            {
                Usuario cliente = usuarioDAO.Usuario_GetByID(idUSuario);
                gestorDAO.cerrarConexion();
                return cliente;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<TipoUsuario> listarTipoUsuario()
        {
            try
            {
                List<TipoUsuario> lista = new List<TipoUsuario>();
                lista = tipoUsuarioDao.listaTipoUsuario();
                gestorDAO.cerrarConexion();
                return lista;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<Sucursal> Sucursal_GetAll()
        {
            try
            {
                List<Sucursal> lista = new List<Sucursal>();
                lista = sucursalDao.Sucursal_GetAll();
                gestorDAO.cerrarConexion();
                return lista;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Boolean Usuario_Delete(Int32 idUsuario)
        {
            Boolean elimina;

            try
            {
                elimina = usuarioDAO.Usuario_Delete(idUsuario);
                gestorDAO.cerrarConexion();
                return elimina;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
    }
}
