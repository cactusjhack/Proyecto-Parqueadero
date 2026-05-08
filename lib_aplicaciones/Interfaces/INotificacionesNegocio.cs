

using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface INotificacionesNegocio
    {
        List<Notificaciones> Consultar();
        Notificaciones Guardar(Notificaciones entidad);
        Notificaciones Actualizar(Notificaciones entidad);
        bool Eliminar(int id);
    }
}
