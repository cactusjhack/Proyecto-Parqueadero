using lib_aplicaciones.nucleo;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.implementaciones
{
    public class IngresosNegocio : IIngresosNegocio
    {
        private IConexion? iConexion;

        public List<Ingresos> Consultar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var lista = this.iConexion.Ingresos!.ToList();

            return lista;
        }

        public Ingresos Guardar(Ingresos entidad)
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

            var espacio = this.iConexion.Espacios!.FirstOrDefault(x => x.Id == entidad.Espacio);
            if (espacio == null)
                throw new Exception("El espacio no existe");

            this.iConexion.Ingresos!.Add(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Ingresos",
                Accion = $"Se guardo el Ingreso con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return entidad;
        }

        public Ingresos Actualizar(Ingresos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("El ingreso no existe");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.iConexion.Ingresos!.Update(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Ingresos",
                Accion = $"Se actualizo el ingreso con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return entidad;
        }

        public bool Eliminar(int id)
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var entidad = this.iConexion.Ingresos!.FirstOrDefault(x => x.Id == id);
            if (entidad == null)
                throw new Exception("El ingreso no existe");


            this.iConexion.Ingresos!.Remove(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Ingresos",
                Accion = $"Se elimino el ingreso con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return true;
        }

    }
}