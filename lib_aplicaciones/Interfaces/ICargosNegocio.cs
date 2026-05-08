using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface ICargosNegocio
    {
        List<Cargos> Consultar();
        Cargos Guardar(Cargos entidad);
        Cargos Actualizar(Cargos entidad);
        bool Eliminar(int id);
    }
}
