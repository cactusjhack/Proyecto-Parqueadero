using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CargosController : ControllerBase
    {
        private ICargosNegocio? ICargosNegocio;

        public CargosController()
        {
            ICargosNegocio = new CargosNegocio();
        }

        [HttpGet]
        public List<Cargos> Consultar()
        {
            if (ICargosNegocio == null)
                throw new Exception("No implementado");
            return ICargosNegocio!.Consultar();
        }

        [HttpPost]
        public Cargos Guardar([FromBody] Cargos entidad)
        {
            if (ICargosNegocio == null)
                throw new Exception("No implementado");
            return ICargosNegocio!.Guardar(entidad);
        }

        [HttpPut]
        public Cargos Actualizar([FromBody] Cargos entidad)
        {
            if (ICargosNegocio == null)
                throw new Exception("No implementado");
            return ICargosNegocio!.Actualizar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            if (ICargosNegocio == null)
                throw new Exception("No implementado");
            return ICargosNegocio!.Eliminar(id);
        }
    }
}
