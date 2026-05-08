using lib_aplicaciones.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_aplicaciones.Interfaces
{
    public interface IDetallesNegocio
    {
        List<Detalles> Consultar();
        Detalles Guardar(Detalles entidad);
        Detalles Actualizar(Detalles entidad);
        bool Eliminar(int id);
    }
}
