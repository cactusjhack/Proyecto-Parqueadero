using lib_aplicaciones.nucleo;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.implementaciones
{
    public class PisosNegocio : IPisosNegocio
    {
        private IConexion? iConexion;

        public List<Pisos> Consultar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var lista = this.iConexion.Pisos!.ToList();

            return lista;
        }

        public Pisos Guardar(Pisos entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var sede = this.iConexion.Sedes!.FirstOrDefault(x => x.Id == entidad.Sede);
            if (sede == null)
                throw new Exception("La sede no existe");

            this.iConexion.Pisos!.Add(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Pisos",
                Accion = $"Se guardo el piso {entidad.Capacidad} con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return entidad;
        }

        public Pisos Actualizar(Pisos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("El piso no existe");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.iConexion.Pisos!.Update(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Pisos",
                Accion = $"Se actualizo el piso {entidad.Capacidad} con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return entidad;
        }

        public bool Eliminar(int id)
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var entidad = this.iConexion.Pisos!.FirstOrDefault(x => x.Id == id);
            if (entidad == null)
                throw new Exception("El convenio no existe");


            this.iConexion.Pisos!.Remove(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Pisos",
                Accion = $"Se elimino el piso {entidad.Capacidad} con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return true;
        }

    }
}