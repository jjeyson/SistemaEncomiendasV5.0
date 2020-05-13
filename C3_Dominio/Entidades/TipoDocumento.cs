using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3_Dominio.Entidades
{
   public class TipoDocumento
    {
       private int _idTipoDocumento;
       private string _descripcion;
       public int IdTipoDocumento
       {
           get { return _idTipoDocumento; }
           set { _idTipoDocumento = value; }
       }

       public string Descripcion
       {
           get { return _descripcion; }
           set { _descripcion = value; }
       }
    }
}
