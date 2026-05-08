using lib_aplicaciones.Entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASP_Servicios.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CobrosController : ControllerBase
    {
        private ICobrosNegocio? ICobrosNegocio;

        public CobrosController()
        {
            ICobrosNegocio = new CobrosNegocio();
        }

        [HttpGet]
        public List<Cobros> Consultar()
        {
            if (ICobrosNegocio == null)
                throw new Exception("No implementado");
            return ICobrosNegocio!.Consultar();
        }

        [HttpPost]
        public Cobros Guardar([FromBody] Cobros entidad)
        {
            if (ICobrosNegocio == null)
                throw new Exception("No implementado");
            return ICobrosNegocio!.Guardar(entidad);
        }

        [HttpPut]
        public Cobros Actualizar([FromBody] Cobros entidad)
        {
            if (ICobrosNegocio == null)
                throw new Exception("No implementado");
            return ICobrosNegocio!.Actualizar(entidad);
        }

        [HttpDelete("{id}")]
        public bool Eliminar(int id)
        {
            if (ICobrosNegocio == null)
                throw new Exception("No implementado");
            return ICobrosNegocio!.Eliminar(id);
        }
    }
}
