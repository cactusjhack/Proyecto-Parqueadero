using lib_aplicaciones.nucleo;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.implementaciones
{
    public class MantenimientosNegocio : IMantenimientosNegocio
    {
        private IConexion? iConexion;

        public List<Mantenimientos> Consultar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var lista = this.iConexion.Mantenimientos!.ToList();

            return lista;
        }

        public Mantenimientos Guardar(Mantenimientos entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var empleado = this.iConexion.Empleados!.FirstOrDefault(x => x.Id == entidad.Empleado);
            if (empleado == null)
                throw new Exception("El empleado no existe");

            var espacio = this.iConexion.Espacios!.FirstOrDefault(x => x.Id == entidad.Espacio);
            if (espacio == null)
                throw new Exception("El espacio no existe");

            this.iConexion.Mantenimientos!.Add(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Mantenimientos",
                Accion = $"Se guardo el Mantenimiento con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return entidad;
        }

        public Mantenimientos Actualizar(Mantenimientos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("El mantenimiento no existe");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.iConexion.Mantenimientos!.Update(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Mantenimientos",
                Accion = $"Se actualizo el mantenimiento con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return entidad;
        }

        public bool Eliminar(int id)
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var entidad = this.iConexion.Mantenimientos!.FirstOrDefault(x => x.Id == id);
            if (entidad == null)
                throw new Exception("El mantenimiento no existe");


            this.iConexion.Mantenimientos!.Remove(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Mantenimientos",
                Accion = $"Se elimino el mantenimiento con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return true;
        }

    }
}