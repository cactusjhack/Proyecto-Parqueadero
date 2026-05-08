using lib_aplicaciones.nucleo;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.implementaciones
{
    public class EmpleadosNegocio : IEmpleadosNegocio
    {
        private IConexion? iConexion;

        public List<Empleados> Consultar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var lista = this.iConexion.Empleados!.ToList();

            return lista;
        }

        public Empleados Guardar(Empleados entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var cargo = this.iConexion.Cargos!.FirstOrDefault(x => x.Id == entidad.Cargo);
            if (cargo == null)
                throw new Exception("El cargo no existe");

            this.iConexion.Empleados!.Add(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Empleados",
                Accion = $"Se guardo el empleado {entidad.Apellido} con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return entidad;
        }

        public Empleados Actualizar(Empleados entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("El empleado no existe");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.iConexion.Empleados!.Update(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Empleados",
                Accion = $"Se actualizo el empleado {entidad.Apellido} con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return entidad;
        }

        public bool Eliminar(int id)
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var entidad = this.iConexion.Empleados!.FirstOrDefault(x => x.Id == id);
            if (entidad == null)
                throw new Exception("El empleado no existe");


            this.iConexion.Empleados!.Remove(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Empleados",
                Accion = $"Se elimino el empleado {entidad.Apellido} con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return true;
        }

    }
}