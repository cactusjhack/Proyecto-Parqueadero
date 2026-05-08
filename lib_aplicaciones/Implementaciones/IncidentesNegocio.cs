using lib_aplicaciones.nucleo;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.implementaciones
{
    public class IncidentesNegocio : IIncidentesNegocio
    {
        private IConexion? iConexion;

        public List<Incidentes> Consultar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var lista = this.iConexion.Incidentes!.ToList();

            return lista;
        }

        public Incidentes Guardar(Incidentes entidad)
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

            this.iConexion.Incidentes!.Add(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Incidentes",
                Accion = $"Se guardo el Incidente con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return entidad;
        }

        public Incidentes Actualizar(Incidentes entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("El incidente no existe");
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.iConexion.Incidentes!.Update(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Incidentes",
                Accion = $"Se actualizo el incidente con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return entidad;
        }

        public bool Eliminar(int id)
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var entidad = this.iConexion.Incidentes!.FirstOrDefault(x => x.Id == id);
            if (entidad == null)
                throw new Exception("El incidente no existe");


            this.iConexion.Incidentes!.Remove(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Incidentes",
                Accion = $"Se elimino el incidente con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return true;
        }

    }
}