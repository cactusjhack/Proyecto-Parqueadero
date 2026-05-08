using lib_aplicaciones.nucleo;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.implementaciones
{
    public class ConveniosNegocio : IConveniosNegocio
    {
        private IConexion? iConexion;

        public List<Convenios> Consultar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var lista = this.iConexion.Convenios!.ToList();

            return lista;
        }

        public Convenios Guardar(Convenios entidad)
        {
            if (entidad.Id != 0)
                throw new Exception("Ya se guardo");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var sede = this.iConexion.Sedes!.FirstOrDefault(x => x.Id == entidad.Sede);
            if (sede == null)
                throw new Exception("La sede no existe");

            this.iConexion.Convenios!.Add(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Convenios",
                Accion = $"Se guardo el convenio {entidad.Empresa} con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return entidad;
        }

        public Convenios Actualizar(Convenios entidad)
        {
            if (entidad.Id == 0)
                throw new Exception("El convenio no existe");

            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            this.iConexion.Convenios!.Update(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Convenios",
                Accion = $"Se actualizo el convenio {entidad.Empresa} con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return entidad;
        }

        public bool Eliminar(int id)
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var entidad = this.iConexion.Convenios!.FirstOrDefault(x => x.Id == id);
            if (entidad == null)
                throw new Exception("El convenio no existe");


            this.iConexion.Convenios!.Remove(entidad!);
            this.iConexion.SaveChanges();

            this.iConexion.Auditorias!.Add(new Auditorias
            {
                Tabla = "Convenios",
                Accion = $"Se elimino el convenio {entidad.Empresa} con id {entidad.Id}",
                Fecha = DateTime.Now
            });

            this.iConexion.SaveChanges();
            return true;
        }

    }
}