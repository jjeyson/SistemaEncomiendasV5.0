using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3_Dominio.Entidades
{
    public class DocumentoEnvioEncomienda
    {
        private int _idDocumentoEnvioEncomienda;
        private Ruta _ruta;
        private Cliente _clienteEnvio;
        private Cliente _clienteEntrega;
        private DateTime _fechaEnvio;
        private DateTime _fechaEntrega;
        private String _aDomicilio;
        private String _estado;
        private String _conductor;
        private Int32 _qtyPackages;
        private Int32 _qtyPackages2;
        private bool _activo;
        public List<DetalleDocumentoEnvioEncomienda> detalleEnvio { get; set; }
        public int IdDocumentoEnvioEncomienda
        {
            get { return _idDocumentoEnvioEncomienda; }
            set { _idDocumentoEnvioEncomienda = value; }
        }
        public Int32 CantidadPaquetes2
        {
            get { return _qtyPackages2; }
            set { _qtyPackages2 = value; }
        }
        public Int32 CantidadPaquetes
        {
            get { return _qtyPackages; }
            set { _qtyPackages = value; }
        }
        public Cliente ClienteEnvio
        {
            get { return _clienteEnvio; }
            set { _clienteEnvio = value; }
        }
        public Cliente ClienteEntrega
        {
            get { return _clienteEntrega; }
            set { _clienteEntrega = value; }
        }
        public DateTime FechaEnvio
        {
            get { return _fechaEnvio; }
            set { _fechaEnvio = value; }
        }
        public DateTime fechaEntrega
        {
            get { return _fechaEntrega; }
            set { _fechaEntrega = value; }
        }
        public string ADomicilio
        {
            get { return _aDomicilio; }
            set { _aDomicilio = value; }
        }
        public string Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }
        public Ruta Ruta
        {
            get { return _ruta; }
            set { _ruta = value; }
        }
        public bool Activo
        {
            get { return _activo; }
            set { _activo = value; }
        }
        public String Conductor
        {
            get { return _conductor; }
            set { _conductor = value; }
        }



    }
}
