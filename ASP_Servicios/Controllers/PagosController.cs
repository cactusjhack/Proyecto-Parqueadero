using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PagosController : ControllerBase
    {
        private IPagosNegocio? IPagosNegocio;

        public PagosController()
        {
            IPagosNegocio = new PagosNegocio();
        }

        [HttpGet]
        public List<Pagos> Consultar()
        {
            if (IPagosNegocio == null)
                throw new Exception("No implementado");
            return IPagosNegocio!.Consultar();
        }

        [HttpPost]
        public Pagos Guardar([FromBody] Pagos entidad)
        {
            if (IPagosNegocio == null)
                throw new Exception("No implementado");
            return IPagosNegocio!.Guardar(entidad);
        }

        [HttpPut]
        public Pagos Actualizar([FromBody] Pagos entidad)
        {
            if (IPagosNegocio == null)
                throw new Exception("No implementado");
            return IPagosNegocio!.Actualizar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            if (IPagosNegocio == null)
                throw new Exception("No implementado");
            return IPagosNegocio!.Eliminar(id);
        }
    }
}
