using lib_aplicaciones.nucleo;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.implementaciones
{
    public class DetallesNegocio : IDetallesNegocio
    {
        private IConexion? iConexion;

        public List<Detalles> Consultar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var lista = this.iConexion.Detalles!.ToList();

            return lista;
        }

        public Detalles Guardar(Detalles entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var vehiculo = this.iConexion.Vehiculos!.FirstOrDefault(x => x.Id == entidad.Vehiculo);
            if (vehiculo == null)
                throw new Exception("El vehiculo no existe");

            var empleado = this.iConexion.Empleados!.FirstOrDefault(x => x.Id == entidad.Empleado);
            if (empleado == null)
                throw new Exception("El empleado no existe");

            this.iConexion.Detalles!.Add(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Detalles",
                Accion = $"Se guardo el detalle con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return entidad;
        }

        public Detalles Actualizar(Detalles entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("El detalle no existe");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.iConexion.Detalles!.Update(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Detalles",
                Accion = $"Se actualizo el detalle con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return entidad;
        }

        public bool Eliminar(int id)
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var entidad = this.iConexion.Detalles!.FirstOrDefault(x => x.Id == id);
            if (entidad == null)
                throw new Exception("El detalle no existe");


            this.iConexion.Detalles!.Remove(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Detalles",
                Accion = $"Se elimino el detalle con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return true;
        }

    }
}