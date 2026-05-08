using lib_aplicaciones.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_aplicaciones.Interfaces
{
    public interface ITarifasNegocio
    {
        List<Tarifas> Consultar();
        Tarifas Guardar(Tarifas entidad);
        Tarifas Actualizar(Tarifas entidad);
        bool Eliminar(int id);
    }
}
