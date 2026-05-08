using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IEspaciosNegocio
    {
        List<Espacios> Consultar();
        Espacios Guardar(Espacios entidad);
        Espacios Actualizar(Espacios entidad);
        bool Eliminar(int id);
    }
}
