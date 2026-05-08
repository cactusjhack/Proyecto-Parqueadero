using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IUsuariosNegocio
    {
        List<Usuarios> Consultar();
        Usuarios Guardar(Usuarios entidad);
        Usuarios Actualizar(Usuarios entidad);
        bool Eliminar(int id);
        Usuarios? Login(string NombreUsuario, string Contrasena);
    }
}
