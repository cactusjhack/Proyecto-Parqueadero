using lib_aplicaciones.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_aplicaciones.Interfaces
{
    public interface IReservasNegocio
    {
        List<Reservas> Consultar();
        Reservas Guardar(Reservas entidad);
        Reservas Actualizar(Reservas entidad);
        bool Eliminar(int id);
    }
}
