using lib_aplicaciones.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_aplicaciones.Interfaces
{
    public interface IValetRegistrosNegocio
    {
        List<ValetRegistros> Consultar();
        ValetRegistros Guardar(ValetRegistros entidad);
        ValetRegistros Actualizar(ValetRegistros entidad);
        bool Eliminar(int id);
    }
}
