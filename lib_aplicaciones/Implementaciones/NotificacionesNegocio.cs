using lib_aplicaciones.nucleo;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.implementaciones
{
    public class NotificacionesNegocio : INotificacionesNegocio
    {
        private IConexion? iConexion;

        public List<Notificaciones> Consultar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var lista = this.iConexion.Notificaciones!.ToList();

            return lista;
        }

        public Notificaciones Guardar(Notificaciones entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var cliente = this.iConexion.Clientes!.FirstOrDefault(x => x.Id == entidad.Cliente);
            if (cliente == null)
                throw new Exception("El cliente no existe");

            this.iConexion.Notificaciones!.Add(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Notificaciones",
                Accion = $"Se guardo la notificacion con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return entidad;
        }

        public Notificaciones Actualizar(Notificaciones entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("La notificacion no existe");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.iConexion.Notificaciones!.Update(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Notificaciones",
                Accion = $"Se actualizo la notificacion con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return entidad;
        }

        public bool Eliminar(int id)
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var entidad = this.iConexion.Notificaciones!.FirstOrDefault(x => x.Id == id);
            if (entidad == null)
                throw new Exception("La notificacion no existe");


            this.iConexion.Notificaciones!.Remove(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Notificaciones",
                Accion = $"Se elimino la notificacion con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return true;
        }

    }
}