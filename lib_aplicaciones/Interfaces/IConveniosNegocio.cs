using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IConveniosNegocio
    {
        List<Convenios> Consultar();
        Convenios Guardar(Convenios entidad);
        Convenios Actualizar(Convenios entidad);
        bool Eliminar(int id);
    }
}
