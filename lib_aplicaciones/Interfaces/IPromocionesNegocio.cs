using lib_aplicaciones.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_aplicaciones.Interfaces
{
    public interface IPromocionesNegocio
    {
        List<Promociones> Consultar();
        Promociones Guardar(Promociones entidad);
        Promociones Actualizar(Promociones entidad);
        bool Eliminar(int id);
    }
}
