using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IMantenimientosNegocio
    {
        List<Mantenimientos> Consultar();
        Mantenimientos Guardar(Mantenimientos entidad);
        Mantenimientos Actualizar(Mantenimientos entidad);
        bool Eliminar(int id);
    }
}
