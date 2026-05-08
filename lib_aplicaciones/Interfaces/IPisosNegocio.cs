using lib_aplicaciones.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_aplicaciones.Interfaces
{
    public interface IPisosNegocio
    {
        List<Pisos> Consultar();
        Pisos Guardar(Pisos entidad);
        Pisos Actualizar(Pisos entidad);
        bool Eliminar(int id);
    }
}
