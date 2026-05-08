using lib_aplicaciones.nucleo;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.implementaciones
{
    public class TiquetesNegocio : ITiquetesNegocio
    {
        private IConexion? iConexion;

        public List<Tiquetes> Consultar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var lista = this.iConexion.Tiquetes!.ToList();

            return lista;
        }

        public Tiquetes Guardar(Tiquetes entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var ingreso = this.iConexion.Ingresos!.FirstOrDefault(x => x.Id == entidad.Ingreso);
            if (ingreso == null)
                throw new Exception("El ingreso no existe");

            this.iConexion.Tiquetes!.Add(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Tiquetes",
                Accion = $"Se guardo el tiquete {entidad.Codigo} con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return entidad;
        }

        public Tiquetes Actualizar(Tiquetes entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("El tiquete no existe");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.iConexion.Tiquetes!.Update(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Tiquetes",
                Accion = $"Se actualizo el tiquete {entidad.Codigo} con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return entidad;
        }

        public bool Eliminar(int id)
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var entidad = this.iConexion.Tiquetes!.FirstOrDefault(x => x.Id == id);
            if (entidad == null)
                throw new Exception("El tiquete no existe");


            this.iConexion.Tiquetes!.Remove(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Tiquetes",
                Accion = $"Se elimino el tiquete {entidad.Codigo} con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return true;
        }

    }
}