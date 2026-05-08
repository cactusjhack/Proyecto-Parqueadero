using lib_aplicaciones.nucleo;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.implementaciones
{
    public class EspaciosNegocio : IEspaciosNegocio
    {
        private IConexion? iConexion;

        public List<Espacios> Consultar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var lista = this.iConexion.Espacios!.ToList();

            return lista;
        }

        public Espacios Guardar(Espacios entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var piso = this.iConexion.Pisos!.FirstOrDefault(x => x.Id == entidad.Piso);
            if (piso == null)
                throw new Exception("El piso no existe");

            this.iConexion.Espacios!.Add(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Espacios",
                Accion = $"Se guardo el espacio {entidad.Numero} con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return entidad;
        }

        public Espacios Actualizar(Espacios entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("El espacio no existe");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.iConexion.Espacios!.Update(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Espacios",
                Accion = $"Se actualizo el espacio {entidad.Numero} con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return entidad;
        }

        public bool Eliminar(int id)
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var entidad = this.iConexion.Espacios!.FirstOrDefault(x => x.Id == id);
            if (entidad == null)
                throw new Exception("El espacio no existe");


            this.iConexion.Espacios!.Remove(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Espacios",
                Accion = $"Se elimino el espacio {entidad.Numero} con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return true;
        }

    }
}