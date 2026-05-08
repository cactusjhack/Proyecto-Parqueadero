using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface ICamarasNegocio
    {
        List<Camaras> Consultar();
        Camaras Guardar(Camaras entidad);
        Camaras Actualizar(Camaras entidad);
        bool Eliminar(int id);
    }
}
