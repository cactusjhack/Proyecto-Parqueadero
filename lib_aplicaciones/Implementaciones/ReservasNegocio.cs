using lib_aplicaciones.nucleo;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.implementaciones
{
    public class ReservasNegocio : IReservasNegocio
    {
        private IConexion? iConexion;

        public List<Reservas> Consultar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var lista = this.iConexion.Reservas!.ToList();

            return lista;
        }

        public Reservas Guardar(Reservas entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var cliente = this.iConexion.Clientes!.FirstOrDefault(x => x.Id == entidad.Cliente);
            if (cliente == null)
                throw new Exception("El cliente no existe");

            var vehiculo = this.iConexion.Vehiculos!.FirstOrDefault(x => x.Id == entidad.Vehiculo);
            if (vehiculo == null)
                throw new Exception("El vehiculo no existe");

            var espacio = this.iConexion.Espacios!.FirstOrDefault(x => x.Id == entidad.Espacio);
            if (espacio == null)
                throw new Exception("El espacio no existe");

            this.iConexion.Reservas!.Add(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Reservas",
                Accion = $"Se guardo la Reserva con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return entidad;
        }

        public Reservas Actualizar(Reservas entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("La reserva no existe");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.iConexion.Reservas!.Update(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Reservas",
                Accion = $"Se actualizo la reserva con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return entidad;
        }

        public bool Eliminar(int id)
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var entidad = this.iConexion.Reservas!.FirstOrDefault(x => x.Id == id);
            if (entidad == null)
                throw new Exception("La reserva no existe");


            this.iConexion.Reservas!.Remove(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Reservas",
                Accion = $"Se elimino la reserva con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return true;
        }

    }
}