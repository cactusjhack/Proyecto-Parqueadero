using lib_aplicaciones.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_aplicaciones.Interfaces
{
    public interface IIncidentesNegocio
    {
        List<Incidentes> Consultar();
        Incidentes Guardar(Incidentes entidad);
        Incidentes Actualizar(Incidentes entidad);
        bool Eliminar(int id);  
    }
}
