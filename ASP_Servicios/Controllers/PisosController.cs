using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PisosController : ControllerBase
    {
        private IPisosNegocio? IPisosNegocio;

        public PisosController()
        {
            IPisosNegocio = new PisosNegocio();
        }

        [HttpGet]
        public List<Pisos> Consultar()
        {
            if (IPisosNegocio == null)
                throw new Exception("No implementado");
            return IPisosNegocio!.Consultar();
        }

        [HttpPost]
        public Pisos Guardar([FromBody] Pisos entidad)
        {
            if (IPisosNegocio == null)
                throw new Exception("No implementado");
            return IPisosNegocio!.Guardar(entidad);
        }

        [HttpPut]
        public Pisos Actualizar([FromBody] Pisos entidad)
        {
            if (IPisosNegocio == null)
                throw new Exception("No implementado");
            return IPisosNegocio!.Actualizar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            if (IPisosNegocio == null)
                throw new Exception("No implementado");
            return IPisosNegocio!.Eliminar(id);
        }
    }
}
