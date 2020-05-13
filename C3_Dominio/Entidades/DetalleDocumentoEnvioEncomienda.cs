using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3_Dominio.Entidades
{
  public   class DetalleDocumentoEnvioEncomienda
    {
      private int _idDetalleDocumentoEnvioEncomienda;
      private DocumentoEnvioEncomienda _documentoEnvio;
      private string _descripcion;
      private int _peso;
      private double _precioGramo;
      public double _subTotal;
      public double _descuento;
      public int IdDetalleDocumentoEnvioEncomienda
      {
          get { return _idDetalleDocumentoEnvioEncomienda; }
          set { _idDetalleDocumentoEnvioEncomienda = value; }
      }
      public double PrecioGramo
      {
          get { return _precioGramo; }
          set { _precioGramo = value; }
      }
      public double Descuento
      {
          get { return _descuento; }
          set { _descuento = value; }
      }
      public double SubTotal
      {
          get { return _subTotal; }
          set { _subTotal = value; }
      }
      public DocumentoEnvioEncomienda DocumentoEnvio
      {
          get { return _documentoEnvio; }
          set { _documentoEnvio = value; }
      }
      public string Descripcion
      {
          get { return _descripcion; }
          set { _descripcion = value; }
      }
      public int Peso
      {
          get { return _peso; }
          set { _peso = value; }
      }

      public double CalcularPrecio()
      {
          return _peso * _precioGramo;
      }

      public double CalcularTotal()
        {

         
          return _subTotal=_subTotal + CalcularPrecio();
      }
      public double CalcularDescuento()
      {
        
          if (SubTotal > 100 && SubTotal <= 200)
          {
              _descuento = SubTotal * 0.1;
          }
          else if (SubTotal > 200)
          {
              _descuento = SubTotal * 0.2;
          }
          else if (SubTotal <= 100)
          {
              _descuento = 0;
          }

          return _descuento;
      }

      public double calcularMontoTotalPagar()
      {

          return SubTotal - CalcularDescuento();
      }


    }
}
