using lib_aplicaciones.nucleo;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.implementaciones
{
    public class CamarasNegocio : ICamarasNegocio
    {
        private IConexion? iConexion;

        public List<Camaras> Consultar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var lista = this.iConexion.Camaras!.ToList();

            return lista;
        }

        public Camaras Guardar(Camaras entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var piso = this.iConexion.Pisos!.FirstOrDefault(x => x.Id == entidad.Piso);
            if (piso == null)
                throw new Exception("La sede no existe");

            this.iConexion.Camaras!.Add(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Camaras",
                Accion = $"Se guardo la camara {entidad.Ubicacion} con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return entidad;
        }

        public Camaras Actualizar(Camaras entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("La camara no existe");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.iConexion.Camaras!.Update(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Camaras",
                Accion = $"Se actualizo la camara {entidad.Ubicacion} con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return entidad;
        }

        public bool Eliminar(int id)
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var entidad = this.iConexion.Camaras!.FirstOrDefault(x => x.Id == id);
            if (entidad == null)
                throw new Exception("La camara no existe"); 

            this.iConexion.Camaras!.Remove(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Camaras",
                Accion = $"Se elimino la camara {entidad.Ubicacion} con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return true;
        }

    }
}