using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3_Dominio.Entidades
{
    public class Sucursal
    {
        private int _idSucursal;
        private string _ciudad;
        private string _direccion;
        private string _telefono;
        private bool _activo;

        public int IdSucursal
        {
            get { return _idSucursal; }
            set { _idSucursal = value; }
        }
        public string Ciudad
        {
            get { return _ciudad; }
            set { _ciudad = value; }
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
