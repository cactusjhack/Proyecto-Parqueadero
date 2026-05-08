using lib_aplicaciones.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_aplicaciones.Interfaces
{
    public interface IEmpleadosNegocio
    {
        List<Empleados> Consultar();
        Empleados Guardar(Empleados entidad);
        Empleados Actualizar(Empleados entidad);
        bool Eliminar(int id);
    }
}
