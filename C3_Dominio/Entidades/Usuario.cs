using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace C3_Dominio.Entidades
{
   public class Usuario
    {
        private int _idUsuario;
        private string _usuario;
        private string _clave;
        private string _nombreUsuario;
        private string _apellidosUsuario;
        private string _DNI;
        private string _direccion;
        private string _telefono;
        private string _cargo;
        private Sucursal _sucursal;
        private bool _activo;
        public TipoUsuario tipoUsuario { get; set; }
        public int IdUsuario
        {
            get { return _idUsuario; }
            set { _idUsuario = value; }
        }
        [Required(ErrorMessage = "Debe introducir un nombre usuario")]
        public string Usuarios
        {
            get { return _usuario; }
            set { _usuario = value; }
        }
    
        [Required(ErrorMessage = "Debe introducir una clave de usuario")]
     
        public string Clave
        {
            get { return _clave; }
            set { _clave = value; }
        }
        public string NombreUsuario
        {
            get { return _nombreUsuario; }
            set { _nombreUsuario = value; }
        }
        public string ApellidosUsuario
        {
            get { return _apellidosUsuario; }
            set { _apellidosUsuario = value; }
        }
        public string DNI
        {
            get { return _DNI; }
            set { _DNI = value; }
        }
        public string Direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }
        public string Telefono
        {
            get { return _telefono; }
            set { _telefono = value; }
        }
        public string Cargo
        {
            get { return _cargo; }
            set { _cargo = value; }
        }
        public Sucursal sucursal
        {
            get { return _sucursal; }
            set { _sucursal = value; }
        }
        public bool Activo
        {
            get { return _activo; }
            set { _activo = value; }
        }
        public Boolean EsActivo()
        {
            if (this._activo == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
  
         

    }
}
