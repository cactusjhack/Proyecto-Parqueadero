using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IPagosNegocio
    {
        List<Pagos> Consultar();
        Pagos Guardar(Pagos entidad);
        Pagos Actualizar(Pagos entidad);
        bool Eliminar(int id);
    }
}
