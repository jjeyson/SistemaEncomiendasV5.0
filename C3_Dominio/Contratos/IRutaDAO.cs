using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C3_Dominio.Entidades;
namespace C3_Dominio.Contratos
{
     public interface IRutaDAO
    {
         List<Ruta> listarRuta();
         List<Ruta> listarRutaId(int idRuta);

      Tarifa listarTarifaIdRuta(int idRuta);
    }
}
