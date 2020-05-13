using C3_Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3_Dominio.Contratos
{
    public interface  ISucursal
    {
        List<Sucursal> Sucursal_GetAll();
    }
}
