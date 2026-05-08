using lib_aplicaciones.nucleo;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.implementaciones
{
    public class ValetRegistrosNegocio : IValetRegistrosNegocio
    {
        private IConexion? iConexion;

        public List<ValetRegistros> Consultar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var lista = this.iConexion.ValetRegistros!.ToList();

            return lista;
        }

        public ValetRegistros Guardar(ValetRegistros entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var empleado = this.iConexion.Empleados!.FirstOrDefault(x => x.Id == entidad.Empleado);
            if (empleado == null)
                throw new Exception("El empleado no existe");

            var vehiculo = this.iConexion.Vehiculos!.FirstOrDefault(x => x.Id == entidad.Vehiculo);
            if (vehiculo == null)
                throw new Exception("El vehiculo no existe");

            var ficho = this.iConexion.Fichos!.FirstOrDefault(x => x.Id == entidad.Ficho);
            if (ficho == null)
                throw new Exception("El espacio no existe");

            this.iConexion.ValetRegistros!.Add(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "ValetRegistros",
                Accion = $"Se guardo el ValetRegistro con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return entidad;
        }

        public ValetRegistros Actualizar(ValetRegistros entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("El ValetRegistro no existe");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.iConexion.ValetRegistros!.Update(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "ValetRegistros",
                Accion = $"Se actualizo el ValetRegistro con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return entidad;
        }

        public bool Eliminar(int id)
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var entidad = this.iConexion.ValetRegistros!.FirstOrDefault(x => x.Id == id);
            if (entidad == null)
                throw new Exception("El ValetRegistro no existe");


            this.iConexion.ValetRegistros!.Remove(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "ValetRegistros",
                Accion = $"Se elimino el ValetRegistro con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return true;
        }

    }
}