using lib_aplicaciones.nucleo;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.implementaciones
{
    public class FichosNegocio : IFichosNegocio
    {
        private IConexion? iConexion;

        public List<Fichos> Consultar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var lista = this.iConexion.Fichos!.ToList();

            return lista;
        }

        public Fichos Guardar(Fichos entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var cliente = this.iConexion.Clientes!.FirstOrDefault(x => x.Id == entidad.Cliente);
            if (cliente == null)
                throw new Exception("El ficho no existe");

            this.iConexion.Fichos!.Add(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Fichos",
                Accion = $"Se guardo el ficho {entidad.Codigo} con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return entidad;
        }

        public Fichos Actualizar(Fichos entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("El ficho no existe");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.iConexion.Fichos!.Update(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Fichos",
                Accion = $"Se actualizo el ficho {entidad.Codigo} con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return entidad;
        }

        public bool Eliminar(int id)
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var entidad = this.iConexion.Fichos!.FirstOrDefault(x => x.Id == id);
            if (entidad == null)
                throw new Exception("El ficho no existe");


            this.iConexion.Fichos!.Remove(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Fichos",
                Accion = $"Se elimino el ficho {entidad.Codigo} con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return true;
        }

    }
}