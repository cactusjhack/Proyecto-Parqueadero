using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IFichosNegocio
    {
        List<Fichos> Consultar();
        Fichos Guardar(Fichos entidad);
        Fichos Actualizar(Fichos entidad);
        bool Eliminar(int id);
    }
}
