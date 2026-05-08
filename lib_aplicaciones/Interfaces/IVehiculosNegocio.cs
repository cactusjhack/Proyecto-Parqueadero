
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IVehiculosNegocio
    {
        List<Vehiculos> Consultar();
        Vehiculos Guardar(Vehiculos entidad);
        Vehiculos Actualizar(Vehiculos entidad);
        bool Eliminar(int id);
    }
}
