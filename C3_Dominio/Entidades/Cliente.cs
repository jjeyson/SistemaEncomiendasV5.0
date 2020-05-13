using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace C3_Dominio.Entidades
{
   public class Cliente
    {
        private int _idCliente;
     
        private string _numDocumento;
        private string _nombreCliente;
        private string _apellidoPaternoCliente;
        private string _apellidoMaternoCliente;
        private string _direccion;
        private string _telefono;
        private bool _activo;
        private string _razonSocial;
       // private TipoDocumento _tipoDocumento;
        private string _tipoDocumento;
        
        public int IdCliente
        {
            get { return _idCliente; }
            set { _idCliente = value; }
        }
        public string RazonSocial
        {
            get { return _razonSocial; }
            set { _razonSocial = value; }
        }
        public string TipoDocumento
        {
            get { return _tipoDocumento; }
            set { _tipoDocumento = value; }
        }
        [Required(ErrorMessage = "El nombre es requerido", AllowEmptyStrings = false)]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "El nombre ")]
        //[Display(Name = "Nombre Cliente", Description = "Juan Jose")]
        public string NombreCliente
        {
            get { return _nombreCliente; }
            set { _nombreCliente = value; }
        }
        [Required(ErrorMessage = "El Apellido Paterno es requerido", AllowEmptyStrings = false)]
        //[Display(Name = "Apellido Paterno Cliente", Description = "Perez")]
        public string ApellidoPaternoCliente
        {
            get { return _apellidoPaternoCliente; }
            set { _apellidoPaternoCliente = value; }
        }
        [Required(ErrorMessage = "El Apellido Paterno es requerido", AllowEmptyStrings = false)]
        //[Display(Name = "Apellido Materno Cliente", Description = "Sanchez")]
        public string ApellidoMaternoCliente
        {
            get { return _apellidoMaternoCliente; }
            set { _apellidoMaternoCliente = value; }
        }
        public string NumeroDocumento
        {
            get { return _numDocumento; }
            set { _numDocumento = value; }

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
        public bool Activo
        {
            get { return _activo; }
            set { _activo = value; }

        }
    }
}
