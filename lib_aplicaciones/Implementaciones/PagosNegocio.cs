using lib_aplicaciones.nucleo;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.implementaciones
{
    public class PagosNegocio : IPagosNegocio
    {
        private IConexion? iConexion;

        public List<Pagos> Consultar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var lista = this.iConexion.Pagos!.ToList();

            return lista;
        }

        public Pagos Guardar(Pagos entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var cobro = this.iConexion.Cobros!.FirstOrDefault(x => x.Id == entidad.Cobro);
            if (cobro == null)
                throw new Exception("El cobro no existe");

            this.iConexion.Pagos!.Add(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Pagos",
                Accion = $"Se guardo el pago {entidad.Id} asociado al cobro {entidad.Cobro}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return entidad;
        }

        public Pagos Actualizar(Pagos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("El pago no existe");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.iConexion.Pagos!.Update(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Pagos",
                Accion = $"Se actualizo el pago {entidad.Id} asociado al cobro {entidad.Cobro}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return entidad;
        }

        public bool Eliminar(int id)
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var entidad = this.iConexion.Pagos!.FirstOrDefault(x => x.Id == id);
            if (entidad == null)
                throw new Exception("El pago no existe");


            this.iConexion.Pagos!.Remove(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Pagos",
                Accion = $"Se elimino el pago {entidad.Id} asociado al cobro {entidad.Cobro}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return true;
        }

    }
}