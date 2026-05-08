using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface ICobrosNegocio
    {
        List<Cobros> Consultar();
        Cobros Guardar(Cobros entidad);
        Cobros Actualizar(Cobros entidad);
        bool Eliminar(int id);
    }
}
