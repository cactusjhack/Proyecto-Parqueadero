using lib_aplicaciones.nucleo;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.implementaciones
{
    public class UsuariosNegocio : IUsuariosNegocio
    {
        private IConexion? iConexion;

        public List<Usuarios> Consultar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var lista = this.iConexion.Usuarios!.ToList();

            return lista;
        }

        public Usuarios Guardar(Usuarios entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var rol = this.iConexion.Roles!.FirstOrDefault(x => x.Id == entidad.Rol);
            if (rol == null)
                throw new Exception("El rol no existe");

            var empleado = this.iConexion.Empleados!.FirstOrDefault(x => x.Id == entidad.Empleado);
            if (empleado == null)
                throw new Exception("El empleado no existe");

            this.iConexion.Usuarios!.Add(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Usuaios",
                Accion = $"Se guardo el Usuario {entidad.NombreUsuario} con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return entidad;
        }

        public Usuarios Actualizar(Usuarios entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("El usuario no existe");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.iConexion.Usuarios!.Update(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Usuarios",
                Accion = $"Se actualizo el usuario {entidad.NombreUsuario} con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return entidad;
        }

        public bool Eliminar(int id)
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var entidad = this.iConexion.Usuarios!.FirstOrDefault(x => x.Id == id);
            if (entidad == null)
                throw new Exception("El usuario no existe");


            this.iConexion.Usuarios!.Remove(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Usuarios",
                Accion = $"Se elimino el convenio {entidad.NombreUsuario} con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return true;
        }

        public Usuarios? Loging(string NombreUsuario, string Contrasena)
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var usuario = this.iConexion.Usuarios!.FirstOrDefault(x => x.NombreUsuario == NombreUsuario && x.Contrasena == Contrasena);
            if (usuario == null)
                throw new Exception("El usuario o la contraseñas son incorrectos");

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Usuarios",
                Accion = $"El usuario {usuario.NombreUsuario} con id {usuario.Id} inicio sesion",
                Fecha = DateTime.Now
            });
            this.iConexion.SaveChanges();
            
            return usuario;
        }

        public Usuarios? Login(string NombreUsuario, string Contrasena)
        {
            return Loging(NombreUsuario, Contrasena);
        }
    }
}