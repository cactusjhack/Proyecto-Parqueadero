using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface ITiquetesNegocio
    {
        List<Tiquetes> Consultar();
        Tiquetes Guardar(Tiquetes entidad);
        Tiquetes Actualizar(Tiquetes entidad);
        bool Eliminar(int id);
    }
}
