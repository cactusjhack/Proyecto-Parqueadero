using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface ISedesNegocio
    {
        List<Sedes> Consultar();
        Sedes Guardar(Sedes entidad);
        Sedes Actualizar(Sedes entidad);
        bool Eliminar(int id);
    }
}
