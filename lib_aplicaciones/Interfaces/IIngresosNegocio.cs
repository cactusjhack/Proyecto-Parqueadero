using lib_aplicaciones.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_aplicaciones.Interfaces
{
    public interface IIngresosNegocio
    {
        List<Ingresos> Consultar();
        Ingresos Guardar(Ingresos entidad);
        Ingresos Actualizar(Ingresos entidad);
        bool Eliminar(int id);
    }
}
