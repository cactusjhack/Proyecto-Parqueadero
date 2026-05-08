using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class IncidentesController : ControllerBase
    {
        private IIncidentesNegocio? IIncidentesNegocio;

        public IncidentesController()
        {
            IIncidentesNegocio = new IncidentesNegocio();
        }

        [HttpGet]
        public List<Incidentes> Consultar()
        {
            if (IIncidentesNegocio == null)
                throw new Exception("No implementado");
            return IIncidentesNegocio!.Consultar();
        }

        [HttpPost]
        public Incidentes Guardar([FromBody] Incidentes entidad)
        {
            if (IIncidentesNegocio == null)
                throw new Exception("No implementado");
            return IIncidentesNegocio!.Guardar(entidad);
        }

        [HttpPut]
        public Incidentes Actualizar([FromBody] Incidentes entidad)
        {
            if (IIncidentesNegocio == null)
                throw new Exception("No implementado");
            return IIncidentesNegocio!.Actualizar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            if (IIncidentesNegocio == null)
                throw new Exception("No implementado");
            return IIncidentesNegocio!.Eliminar(id);
        }
    }
}
