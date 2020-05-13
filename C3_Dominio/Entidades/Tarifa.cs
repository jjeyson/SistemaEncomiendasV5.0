using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3_Dominio.Entidades
{
    public class Tarifa
    {
        private int _idTarifa;
        private Ruta _ruta;
        private string _descripcion;
        private double _precioGramo;
        private bool _activo;


        public int IdTarifa
        {
            get { return _idTarifa; }
            set { _idTarifa = value; }
        }
      
        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        public double PrecioGramo
        {
            get { return _precioGramo; }
            set { _precioGramo = value; }
        }
        public bool Activo
        {
            get { return _activo; }
            set { _activo = value; }
        }

    }
}
