using lib_aplicaciones.Entidades;
using lib_aplicaciones.Interfaces;
using lib_aplicaciones.nucleo;

namespace lib_aplicaciones.Implementaciones
{
    public class AuditoriasNegocio : IAuditoriasNegocio
    {
        private IConexion? iConexion;

        public List<Auditorias> Consultar()
        {
            this.iConexion = new Conexion();
            this.iConexion.StringConexion = Configuraciones.obtener("StringConexion");

            var lista = this.iConexion.Auditorias!.ToList();

            return lista;
        }
    }
}
