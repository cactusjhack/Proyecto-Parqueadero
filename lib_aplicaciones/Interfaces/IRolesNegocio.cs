using lib_aplicaciones.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_aplicaciones.Interfaces
{
    public interface IRolesNegocio
    {
        List<Roles> Consultar();
        Roles Guardar(Roles entidad);
        Roles Actualizar(Roles entidad);
        bool Eliminar(int id);
    }
}
