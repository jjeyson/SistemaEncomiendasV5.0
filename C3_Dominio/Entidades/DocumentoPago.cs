using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3_Dominio.Entidades
{
   public class DocumentoPago
    {
       private int _idDocumentoPago;
        private DocumentoEnvioEncomienda _documentoEnvio;
        private string _numSerie;
        private string _numDocumento;
        private string _tipoDocumentoPago;
        private double _monto;
        private double _montoTotal;
        public int IdDocumentoPago
        {
            get { return _idDocumentoPago; }
            set { _idDocumentoPago = value; }
        }
        public DocumentoEnvioEncomienda DocumentoEnvio
        {
            get { return _documentoEnvio; }
            set { _documentoEnvio = value; }
        }
        public string NumSerie
        {
            get { return _numSerie; }
            set { _numSerie = value; }
        }
        public string NumDocumento
        {
            get { return _numDocumento; }
            set { _numDocumento = value; }
        }

        public string TipoDocumento
        {
            get { return _tipoDocumentoPago; }
            set { _tipoDocumentoPago = value; }
        }
        public double Monto
        {
            get { return _monto; }
            set { _monto = value; }
        }
        public double MontoTotal
        {
            get { return _montoTotal; }
            set { _montoTotal = value; }
        }
        //public double CalcularPago()
        //{

        //    double _calcularPago = 0;
        //    return _calcularPago=_documentoEnvio;
        //}
    }
}
