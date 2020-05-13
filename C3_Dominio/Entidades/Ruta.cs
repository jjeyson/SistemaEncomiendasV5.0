using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3_Dominio.Entidades
{
   public class Ruta
    {
        private int _idRuta;
        private Sucursal _sucursalOrigen;
        private Sucursal _sucursalDestino;
        private Tarifa _tarifa;
        private string _descripcion;
        public int IdRuta
        {
            get { return _idRuta; }
            set { _idRuta = value; }
        }
        public Tarifa Tarifas
        {
            get { return _tarifa; }
            set { _tarifa = value; }
        }
        public Sucursal SucursalOrigen
        {
            get { return _sucursalOrigen; }
            set { _sucursalOrigen = value; }
        }
        public Sucursal SucursalDestino
        {
            get { return _sucursalDestino; }
            set { _sucursalDestino= value; }
        }
        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        } 
   }
}
