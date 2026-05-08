using lib_aplicaciones.nucleo;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.implementaciones
{
    public class CobrosNegocio : ICobrosNegocio
    {
        private IConexion? iConexion;

        public List<Cobros> Consultar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var lista = this.iConexion.Cobros!.ToList();

            return lista;
        }

        public Cobros Guardar(Cobros entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var ingreso = this.iConexion.Ingresos!.FirstOrDefault(x => x.Id == entidad.Ingreso);
            if (ingreso == null)
                throw new Exception("El ingreso no existe");

            var cliente = this.iConexion.Clientes!.FirstOrDefault(x => x.Id == entidad.Cliente);
            if (cliente == null)
                throw new Exception("El cliente no existe");

            var tarifa = this.iConexion.Tarifas!.FirstOrDefault(x => x.Id == entidad.Tarifa);
            if (tarifa == null)
                throw new Exception("La tarifa no existe");

            var promocion = this.iConexion.Promociones!.FirstOrDefault(x => x.Id == entidad.Promocion);
            if (promocion == null)
                throw new Exception("La promoción no existe");

            this.iConexion.Cobros!.Add(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Cobros",
                Accion = $"Se guardo el Cobro con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return entidad;
        }

        public Cobros Actualizar(Cobros entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("El cobro no existe");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.iConexion.Cobros!.Update(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Cobros",
                Accion = $"Se actualizo el cobro con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return entidad;
        }

        public bool Eliminar(int id)
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var entidad = this.iConexion.Cobros!.FirstOrDefault(x => x.Id == id);
            if (entidad == null)
                throw new Exception("El cobro no existe");

            this.iConexion.Cobros!.Remove(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Cobros",
                Accion = $"Se elimino el cobro con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return true;
        }

    }
}