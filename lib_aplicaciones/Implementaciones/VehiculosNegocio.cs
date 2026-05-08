using lib_aplicaciones.nucleo;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.implementaciones
{
    public class VehiculosNegocio : IVehiculosNegocio
    {
        private IConexion? iConexion;

        public List<Vehiculos> Consultar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var lista = this.iConexion.Vehiculos!.ToList();

            return lista;
        }

        public Vehiculos Guardar(Vehiculos entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var cliente = this.iConexion.Clientes!.FirstOrDefault(x => x.Id == entidad.Cliente);
            if (cliente == null)
                throw new Exception("El cliente no existe");

            this.iConexion.Vehiculos!.Add(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Vehiculos",
                Accion = $"Se guardo el vehiculo {entidad.Placa} con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return entidad;
        }

        public Vehiculos Actualizar(Vehiculos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("El vehiculo no existe");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.iConexion.Vehiculos!.Update(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Vehiculos",
                Accion = $"Se actualizo el vehiculo {entidad.Placa} con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return entidad;
        }

        public bool Eliminar(int id)
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var entidad = this.iConexion.Vehiculos!.FirstOrDefault(x => x.Id == id);
            if (entidad == null)
                throw new Exception("El vehiculo no existe");


            this.iConexion.Vehiculos!.Remove(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Vehiculos",
                Accion = $"Se elimino el vehiculo {entidad.Placa} con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return true;
        }

    }
}